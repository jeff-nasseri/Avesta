using Avesta.Attribute.Controller;
using Avesta.Constant;
using Avesta.Data.Model;
using Avesta.Exceptions;
using Avesta.Services;
using Avesta.Share.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Avesta.Controller.MVC.Crud
{


    [AvestaMVCController]
    public class CrudController<TId, TEntity, TModel, TCreateModel, TEditModel> : CrudController<TId, TEntity, TModel>, ICrudController<TId, TEntity, TModel, TCreateModel, TEditModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TCreateModel : TModel
        where TEditModel : TModel
    {

        readonly IEntityService<TId, TEntity, TModel, TCreateModel, TEditModel> _entityService;

        public CrudController(IEntityService<TId, TEntity, TModel, TCreateModel, TEditModel> entityService) : base(entityService)
        {
            _entityService = entityService;
        }

        [HttpPut]
        public async Task<IActionResult> Create(TCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _entityService.Insert(model);
                    return RedirectToAction(nameof(GetAll));
                }
                catch (SystemException exception)
                {
                    TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                    ErrorManager.LogExceptionToFile(exception);
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(TEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _entityService.Update(model);
                    return RedirectToAction(nameof(GetAll));
                }
                catch (SystemException exception)
                {
                    TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                    ErrorManager.LogExceptionToFile(exception);
                }
            }

            return View(model);
        }
    }



    [AvestaMVCController]
    public class CrudController<TId, TEntity, TModel> : AvestaBaseMVCController, ICrudController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {

        readonly IEntityService<TId, TEntity, TModel> _entityService;
        public CrudController(IEntityService<TId, TEntity, TModel> entityService)
        {
            _entityService = entityService;
        }


        [HttpPut]
        public async Task<IActionResult> Create(TModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _entityService.Insert(model);
                    return RedirectToAction(nameof(GetAll));
                }
                catch (SystemException exception)
                {
                    TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                    ErrorManager.LogExceptionToFile(exception);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await Task.CompletedTask;
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> Get(TId id)
        {
            try
            {
                var result = await _entityService.Get(id, includeAllPath: false);
                return View(result);
            }
            catch (SystemException exception)
            {
                TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
            }

            return RedirectToAction(nameof(GetAll));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(int? page = null, int perPage = 7, string[] keywords = null)
        {
            var result = await _entityService.GetAll(includeAllPath: false, page: page, perPage: perPage, keywords: keywords);
            return View(result);
        }




        [HttpPost]
        public async Task<IActionResult> Update(TModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _entityService.Update(model);
                    return RedirectToAction(nameof(GetAll));
                }
                catch (SystemException exception)
                {
                    TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                    ErrorManager.LogExceptionToFile(exception);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(TId id)
        {
            try
            {
                var result = await _entityService.Get(id, false);
                return View(result);
            }
            catch (SystemException exception)
            {
                TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
            }

            return RedirectToAction(nameof(GetAll));
        }





        [HttpDelete]
        public async Task<IActionResult> SoftDelete(TId id)
        {
            try
            {
                await _entityService.SoftDelete(id);
                return RedirectToAction(nameof(GetAll));
            }
            catch (SystemException exception)
            {
                TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
            }
            return RedirectToAction(nameof(GetAll));
        }




        [HttpDelete]
        public async Task<IActionResult> Delete(TId id)
        {
            try
            {
                await _entityService.Delete(id);
                return RedirectToAction(nameof(GetAll));
            }
            catch (SystemException exception)
            {
                TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
            }
            return RedirectToAction(nameof(GetAll));
        }


    }
}
