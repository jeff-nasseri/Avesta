using Avesta.Auth.User.Service;
using Avesta.Data.Model;
using Avesta.Share.Model.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagerEndPointController = Avesta.Constant.EndPoints.User.UserManagerController;

namespace Avesta.Controller.API.User
{
    public class UserManagerController<TAvestaUser, TUserViewModel> : AvestaController
        where TAvestaUser : AvestaUser
        where TUserViewModel : UserBaseModel
    {
        readonly IUserService<TAvestaUser> _userService;
        public UserManagerController(IUserService<TAvestaUser> userService)
        {
            _userService = userService;
        }



        [HttpGet]
        [Route(UserManagerEndPointController.GetAll)]
        public virtual async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }



        [HttpGet]
        [Route(UserManagerEndPointController.GetByEmail)]
        public virtual async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            return Ok(user);
        }



        [HttpGet]
        [Route(UserManagerEndPointController.GetById)]
        public virtual async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }



        [HttpPost]
        [Route(UserManagerEndPointController.Edit)]
        public virtual async Task<IActionResult> EditUser(TUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Update(viewModel);
                return Ok(result);
            }
            return BadRequest(viewModel);
        }



        [HttpDelete]
        [Route(UserManagerEndPointController.Delete)]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }




    }
}
