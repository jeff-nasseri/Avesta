using Avesta.Services;
using Avesta.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avesta.Model.API;

namespace Avesta.Controller
{



    public interface IBaseController
    {

        Task<IActionResult> Search(string keyword);
        Task<IActionResult> Paginate(int page, string keyword = null);
    }
    public abstract class BaseController<T> : Microsoft.AspNetCore.Mvc.Controller
        where T : class
    {
        readonly IBaseService<T> _baseService;
        public BaseController(
            IBaseService<T> baseService)
        {
            _baseService = baseService;
        }


        public async Task<RedirectResult> BaseRedirect(string action, string controller, string redirectURL = "")
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(redirectURL))
            {
                return Redirect($"{controller}/{action}");
            }
            return Redirect(redirectURL);
        }
        public async Task<RedirectResult> BaseRedirect(string defaultRedirect, string redirectURL = "")
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(redirectURL))
            {
                return Redirect(defaultRedirect);
            }
            return Redirect(redirectURL);
        }
        public async Task<IActionResult> BaseRedirectToAction(string defaultAction, string redirect = "")
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(redirect))
            {
                return RedirectToAction(defaultAction);
            }
            return Redirect(redirect);

        }


        public async Task<IActionResult> Paginate(int page, string viewName, string keyword = null)
        {
            var tuple = await _baseService.Paginate(page, searchKeyWord: keyword);
            var entities = tuple.Item1;
            TempData["EntityCount"] = tuple.Item2;
            return View(viewName, entities);
        }

        public async Task<IActionResult> Search(string contoller, string keyword)
        {
            //make standard
            keyword = keyword?.Trim();

            await Task.CompletedTask;
            const int _1 = 1;
            return Redirect($"{contoller}/{Storage.Constant.BaseController.Paginate}/{_1}/{keyword}");
        }

        public async Task<IActionResult> Search(string keyword, object _ = null)
        {
            //make standard
            keyword = keyword?.Trim();

            await Task.CompletedTask;
            const int _1 = 1;
            return RedirectToAction(Storage.Constant.BaseController.Paginate, new { page = _1, keyword = keyword });
        }


        public IActionResult Ok(object data)
        {
            var response = new ResponseModel().Success(data);

            return base.Ok(response);
        }

    }


}
