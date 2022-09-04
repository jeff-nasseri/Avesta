﻿
using Avesta.Model;
using Avesta.Model.Data;
using Avesta.Services;
using Avesta.Share.MVC;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudEndPointController = Avesta.Storage.Constant.EndPoints.CrudController;
using Avesta.Exceptions;
using Avesta.Storage.Constant;
using SystemException = Avesta.Exceptions.SystemException;
using Avesta.Language;

namespace Avesta.MVC
{
    public interface ICrudController<TCreateViewModel, TEditViewModel> where TCreateViewModel : BaseModel
  where TEditViewModel : BaseModel
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> Edit(TEditViewModel viewModel, string redirect = null);
        Task<IActionResult> Edit(string id);
        Task<IActionResult> Create();
        Task<IActionResult> Create(TCreateViewModel viewModel, string redirect = null);
        Task<IActionResult> Delete(string id);

    }


    public class CrudController<TModel, TViewModel, TEditViewModel, TCreateViewModel> : BaseController<TModel>, ICrudController<TCreateViewModel, TEditViewModel>
        where TModel : BaseEntity
        where TViewModel : BaseModel
        where TEditViewModel : TViewModel
        where TCreateViewModel : TViewModel
    {
        readonly ICrudServices<TModel, TViewModel, TEditViewModel, TCreateViewModel> _crudService;
        public CrudController(ICrudServices<TModel, TViewModel, TEditViewModel, TCreateViewModel> crudService) : base(crudService)
        {
            _crudService = crudService;
        }

        [Route(CrudEndPointController.Create)]
        public virtual async Task<IActionResult> Create()
        {
            await Task.CompletedTask;
            return View();
        }

        [HttpPost]
        [Route(CrudEndPointController.Create)]
        public virtual async Task<IActionResult> Create(TCreateViewModel viewModel, string redirect = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _crudService.CreateNew(viewModel);
                    TempData[ExceptionKeys.SuccessKey] = Lang.T(PublicMessageKey.SuccessMessage);
                    return await base.BaseRedirectToAction(nameof(GetAll), redirect);
                }
                catch (SystemException exception)
                {
                    TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                    ErrorManager.LogExceptionToFile(exception);
                }
            }
            return View(viewModel);
        }

        [Route(CrudEndPointController.Delete + "/{id}")]
        public virtual async Task<IActionResult> Delete([Required] string id)
        {
            try
            {
                await _crudService.Delete(id);
                TempData[ExceptionKeys.SuccessKey] = Lang.T(PublicMessageKey.SuccessMessage);
            }
            catch (SystemException exception)
            {
                TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                ErrorManager.LogExceptionToFile(exception);
            }
            return RedirectToAction(nameof(GetAll));

        }

        [HttpPost]
        [Route(CrudEndPointController.Edit)]
        public virtual async Task<IActionResult> Edit(TEditViewModel viewModel, string redirect = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _crudService.EditEntity(viewModel);
                    TempData[ExceptionKeys.SuccessKey] = Lang.T(PublicMessageKey.SuccessMessage);
                    return await base.BaseRedirectToAction(nameof(GetAll), redirect);
                }
                catch (SystemException exception)
                {
                    TempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(exception.Code);
                    ErrorManager.LogExceptionToFile(exception);
                }

            }
            return View(viewModel);
        }

        [Route(CrudEndPointController.Edit + "/{id}")]
        public virtual async Task<IActionResult> Edit(string id)
        {
            var result = await _crudService.GetEntityAsViewModel(id);
            return View(result);
        }

        [Route(CrudEndPointController.GetAll)]
        public virtual async Task<IActionResult> GetAll()
        {
            await Task.CompletedTask;
            return RedirectToAction(CrudEndPointController.Paginate, new { page = 1 });
        }




        [Route(CrudEndPointController.Search)]
        public async Task<IActionResult> Search(string keyword)
        {
            return await base.Search(keyword);
        }


        [Route(CrudEndPointController.Paginate)]
        public virtual async Task<IActionResult> Paginate(int page, string keyword = null)
        {
            return await base.Paginate(page, nameof(GetAll), keyword);
        }


    }

}