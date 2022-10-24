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
    public abstract class BaseController<T> : AvestaController
        where T : class
    {
        readonly IBaseService<T> _baseService;
        public BaseController(
            IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

  
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<IActionResult> Paginate(int page, string viewName, string keyword = null)
        {
            var tuple = await _baseService.Paginate(page, searchKeyWord: keyword);
            var entities = tuple.Item1;
            TempData["EntityCount"] = tuple.Item2;
            return View(viewName, entities);
        }

    }







}
