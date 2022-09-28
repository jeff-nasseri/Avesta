using Avesta.Data.Model;
using Avesta.Model;
using Avesta.Services;
using Avesta.Share.MVC;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudEndPointController = Avesta.Storage.Constant.EndPoints.CrudController;

namespace Avesta.MVC.API
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


    public class CrudAPIController<TModel, TViewModel, TEditViewModel, TCreateViewModel> : ControllerBase
        where TCreateViewModel : TViewModel
        where TEditViewModel : TViewModel
        where TViewModel : BaseModel
        where TModel : BaseEntity
    {

        readonly ICrudService<TModel, TViewModel, TEditViewModel, TCreateViewModel> _crudService;
        public CrudAPIController(ICrudService<TModel, TViewModel, TEditViewModel, TCreateViewModel> crudService)
        {
            _crudService = crudService;
        }


        [HttpGet]
        [Route(CrudEndPointController.GetAllWithChildren)]
        public async Task<IEnumerable<TModel>> GetAllWithChildren()
        {
            var result = await _crudService.GetAllEntitiesWithAllChildren();
            return result;
        }


        [HttpGet]
        [Route(CrudEndPointController.GetWithChildren)]
        public async Task<TModel> GetWithChildren(string id)
        {
            var result = await _crudService.GetEntityWithAllChildren(id);
            return result;
        }




        [HttpPost]
        [Route(CrudEndPointController.GetAllByParentId)]
        public async Task<IEnumerable<TViewModel>> GetAllByParentInfo(ParentInfo parent)
        {
            var result = await _crudService.GetAllByParentInfo(parent);
            return result;
        }






        [HttpPost]
        [Route(CrudEndPointController.Create)]
        public async Task<TCreateViewModel> Create(TCreateViewModel viewModel)
        {
            await _crudService.CreateNew(viewModel);
            return viewModel;
        }

        [HttpDelete]
        [Route(CrudEndPointController.Delete)]
        public async Task<TViewModel> Delete(string id)
        {
            var viewModel = await _crudService.GetEntityAsViewModel(id, exceptionRaseIfNotExist: true);
            await _crudService.Delete(id);
            return viewModel;
        }


        [HttpDelete]
        [Route(CrudEndPointController.SoftDelete)]
        public async Task<TViewModel> SoftDelete(string id)
        {
            var viewModel = await _crudService.GetEntityAsViewModel(id, exceptionRaseIfNotExist: true);
            await _crudService.SoftDelete(id);
            return viewModel;
        }


        [HttpPost]
        [Route(CrudEndPointController.Edit)]
        public async Task<TEditViewModel> Edit(TEditViewModel viewModel)
        {
            await _crudService.EditEntity(viewModel);
            return viewModel;
        }

        [HttpGet]
        [Route(CrudEndPointController.GetAll)]
        public async Task<IEnumerable<TViewModel>> GetAll()
        {
            var result = await _crudService.GetAllEntitiesAsViewModel();
            return result;
        }

        [HttpGet]
        [Route(CrudEndPointController.GetAsViewModel)]
        public async Task<TViewModel> GetAsViewModel(string id)
        {
            var result = await _crudService.GetEntityAsViewModel(id, exceptionRaseIfNotExist: true);
            return result;
        }


        [HttpGet]
        [Route(CrudEndPointController.Get)]
        public async Task<TModel> Get(string id)
        {
            var result = await _crudService.GetEntity(id, exceptionRaseIfNotExist: true);
            return result;
        }

    }
}
