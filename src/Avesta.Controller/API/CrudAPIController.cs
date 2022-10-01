using Avesta.Data.Model;
using Avesta.Model;
using Avesta.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudEndPointController = Avesta.Storage.Constant.EndPoints.CrudController;

namespace Avesta.Controller.API
{
    public interface ICrudAPIController<TViewModel, TCreateViewModel, TEditViewModel>
        where TCreateViewModel : TViewModel
        where TEditViewModel : TViewModel
        where TViewModel : BaseModel
    {
        Task<IEnumerable<TViewModel>> GetAll();
        Task<TEditViewModel> Edit(TEditViewModel viewModel);
        Task<TCreateViewModel> Create(TCreateViewModel viewModel);
        Task<TViewModel> Delete(string id);

    }


    public class CrudAPIController<TModel, TViewModel, TEditViewModel, TCreateViewModel> : BaseController<TModel>
        where TCreateViewModel : TViewModel
        where TEditViewModel : TViewModel
        where TViewModel : BaseModel
        where TModel : BaseEntity
    {

        readonly ICrudService<TModel, TViewModel, TEditViewModel, TCreateViewModel> _crudService;
        public CrudAPIController(ICrudService<TModel, TViewModel, TEditViewModel, TCreateViewModel> crudService)
            : base(crudService)
        {
            _crudService = crudService;
        }


        [HttpGet]
        [Route(CrudEndPointController.GetAllWithChildren)]
        public async Task<IActionResult> GetAllWithChildren()
        {
            var result = await _crudService.GetAllEntitiesWithAllChildren();
            return Ok(result);
        }


        [HttpGet]
        [Route(CrudEndPointController.GetWithChildren)]
        public async Task<IActionResult> GetWithChildren(string id)
        {
            var result = await _crudService.GetEntityWithAllChildren(id);
            return Ok(result);
        }



        [HttpGet]
        [Route(CrudEndPointController.GetAllWithSpecificChildren)]
        public async Task<IActionResult> GetAllWithSpecificChildren(string navigationPropertyPath)
        {
            var result = await _crudService.GetAllEntitiesWithSpecificChildren(navigationPropertyPath);
            return Ok(result);
        }



        [HttpGet]
        [Route(CrudEndPointController.GetWithSpecificChildren)]
        public async Task<IActionResult> GetWithSpecificChildren(string id, string navigationPropertyPath)
        {
            var result = await _crudService.GetEntityWithSpecificChildren(id, navigationPropertyPath);
            return Ok(result);
        }




        [HttpPost]
        [Route(CrudEndPointController.GetAllByParentId)]
        public async Task<IActionResult> GetAllByParentInfo(PropertyInfo parent)
        {
            var result = await _crudService.GetAllByParentInfo(parent);
            return Ok(result);
        }






        [HttpPost]
        [Route(CrudEndPointController.Create)]
        public async Task<IActionResult> Create(TCreateViewModel viewModel)
        {
            await _crudService.CreateNew(viewModel);
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route(CrudEndPointController.Delete)]
        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await _crudService.GetEntityAsViewModel(id, exceptionRaseIfNotExist: true);
            await _crudService.Delete(id);
            return Ok(viewModel);
        }


        [HttpDelete]
        [Route(CrudEndPointController.SoftDelete)]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var viewModel = await _crudService.GetEntityAsViewModel(id, exceptionRaseIfNotExist: true);
            await _crudService.SoftDelete(id);
            return Ok(viewModel);
        }


        [HttpPost]
        [Route(CrudEndPointController.Edit)]
        public async Task<IActionResult> Edit(TEditViewModel viewModel)
        {
            await _crudService.EditEntity(viewModel);
            return Ok(viewModel);
        }

        [HttpGet]
        [Route(CrudEndPointController.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _crudService.GetAllEntitiesAsViewModel();
            return Ok(result);
        }

        [HttpGet]
        [Route(CrudEndPointController.GetAsViewModel)]
        public async Task<IActionResult> GetAsViewModel(string id)
        {
            var result = await _crudService.GetEntityAsViewModel(id, exceptionRaseIfNotExist: true);
            return Ok(result);
        }


        [HttpGet]
        [Route(CrudEndPointController.Get)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _crudService.GetEntity(id, exceptionRaseIfNotExist: true);
            return Ok(result);
        }

    }
}
