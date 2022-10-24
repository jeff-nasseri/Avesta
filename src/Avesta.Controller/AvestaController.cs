using Avesta.Model.API;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Controller
{




    public class AvestaController : Microsoft.AspNetCore.Mvc.Controller
    {


        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<RedirectResult> BaseRedirect(string action, string controller, string redirectURL = "")
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(redirectURL))
            {
                return Redirect($"{controller}/{action}");
            }
            return Redirect(redirectURL);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<RedirectResult> BaseRedirect(string defaultRedirect, string redirectURL = "")
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(redirectURL))
            {
                return Redirect(defaultRedirect);
            }
            return Redirect(redirectURL);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<IActionResult> BaseRedirectToAction(string defaultAction, string redirect = "")
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(redirect))
            {
                return RedirectToAction(defaultAction);
            }
            return Redirect(redirect);

        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<IActionResult> Search(string contoller, string keyword)
        {
            //make standard
            keyword = keyword?.Trim();

            await Task.CompletedTask;
            const int _1 = 1;
            return Redirect($"{contoller}/{Storage.Constant.BaseController.Paginate}/{_1}/{keyword}");
        }





        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<IActionResult> Search(string keyword, object _ = null)
        {
            //make standard
            keyword = keyword?.Trim();

            await Task.CompletedTask;
            const int _1 = 1;
            return RedirectToAction(Storage.Constant.BaseController.Paginate, new { page = _1, keyword = keyword });
        }




        [ApiExplorerSettings(IgnoreApi = true)]
        public override OkObjectResult Ok(object data)
        {
            var response = new ResponseModel().Success(data);
            return base.Ok(response);
        }
    }


}
