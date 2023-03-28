using Avesta.Attribute.Graph;
using Avesta.Data.Model;
using Avesta.Services;
using Avesta.Share.Model;
using Avesta.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrudEndPointController = Avesta.Constant.EndPoints.CrudController;

namespace Avesta.Controller.API
{


    public class CrudController<TId, TEntity, TModel> : AvestaAPIController, ICrudController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {

        readonly IEntityService<TId,TEntity, TModel> _entityService;
        public CrudController(IEntityService<TId, TEntity, TModel> entityService)
        {
            _entityService = entityService;
        }


        public Task<IActionResult> Create(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Get(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAll(int? page = null, int? perPage = 7, string[] keyword = null)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(TModel model)
        {
            throw new NotImplementedException();
        }
    }





    public interface ICrudController<TId, TEntity, TModel> : IReadController<TId, TEntity, TModel>
        , ICreateController<TId, TEntity, TModel>
        , IUpdateController<TId, TEntity, TModel>
        , IDeleteController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
    }


    public interface ICrudController<TId, TEntity, TModel, TCreateModel, TEditModel> : ICrudController<TId, TEntity, TModel>
        , IUpdateController<TId, TEntity, TModel, TEditModel>
        , ICreateController<TId, TEntity, TModel, TCreateModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TEditModel : TModel
        where TCreateModel : TModel
    {
    }


    public interface IReadController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {

        Task<IActionResult> Get(TId id);
        Task<IActionResult> GetAll(int? page = null, int? perPage = Pagination.PerPage, string[] keyword = null);
    }




    public interface ICreateController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
        Task<IActionResult> Create(TModel model);
    }
    public interface ICreateController<TId, TEntity, TModel, TCreateModel> : ICreateController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TCreateModel : TModel
    {
        Task<IActionResult> Create(TCreateModel model);
    }




    public interface IUpdateController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
        Task<IActionResult> Update(TModel model);
    }
    public interface IUpdateController<TId, TEntity, TModel, TEditModel> : IUpdateController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TEditModel : TModel
    {
        Task<IActionResult> Update(TEditModel model);
    }




    public interface IDeleteController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
        Task<IActionResult> Delete(TId id);
    }














    public class CrudController : AvestaAPIController
    {

    }

    public class AvestaAPIController : AvestaController
    {

    }




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


    public class CrudAPIController<TModel, TViewModel, TEditViewModel, TCreateViewModel> : BaseAPIController<TModel, TViewModel>
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


        [AvestaGraph]
        [Route(CrudEndPointController.Query)]
        public virtual async Task<IActionResult> Query(
            string? navigationPropertyPath = null
            , string? where = "true"
            , string? select = "item=>item"
            , string? orderBy = nameof(BaseEntity.CreatedDate)
            , int? takeFromLast = null
            , int? page = null
            , int? perpage = Pagination.PerPage)
        {
            var result = await _crudService.PaginateDynamicQuery(navigationPropertyPath, where, select, orderBy, takeFromLast, page, perpage);
            return Ok(result);
        }



        [HttpGet]
        [Route(CrudEndPointController.GetAllWithChildren)]
        public virtual async Task<IActionResult> GetAllWithChildren(int? page = null, int perPage = Pagination.PerPage, string? search = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            await Task.CompletedTask;
            return RedirectToAction(nameof(base.PaginateNavigationChildren), new
            {
                page = page
                ,
                navigationAll = true
                ,
                perPage = perPage
                ,
                keyword = search
                ,
                startDate = startDate
                ,
                endDate = endDate
            });
        }


        [HttpGet]
        [Route(CrudEndPointController.GetWithChildren)]
        public virtual async Task<IActionResult> GetWithChildren(string id)
        {
            var result = await _crudService.GetEntityWithAllChildren(id);
            return Ok(result);
        }


        [HttpPost]
        [Route(CrudEndPointController.UpdateOrCreate)]
        public virtual async Task<IActionResult> UpdateOrCreate(TEditViewModel editViewModel)
        {
            var result = await _crudService.UpdateOrInsert(editViewModel);
            return Ok(result);
        }



        [HttpGet]
        [Route(CrudEndPointController.GetAllWithSpecificChildren)]
        public virtual async Task<IActionResult> GetAllWithSpecificChildren(string navigationPropertyPath, int? page = null, int perPage = Pagination.PerPage
            , string? search = null, string? dynamicQuery = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            await Task.CompletedTask;
            return RedirectToAction(nameof(base.PaginateNavigationChildren), new
            {
                page = page
                ,
                navigation = navigationPropertyPath
                ,
                perPage = perPage
                ,
                keyword = search
                ,
                dynamicQuery = dynamicQuery
                ,
                startDate = startDate
                ,
                endDate = endDate
            });

        }



        [HttpGet]
        [Route(CrudEndPointController.GetWithSpecificChildren)]
        public virtual async Task<IActionResult> GetWithSpecificChildren(string id, string navigationPropertyPath)
        {
            var result = await _crudService.GetEntityWithSpecificChildren(id, navigationPropertyPath);
            return Ok(result);
        }




        [HttpPost]
        [Route(CrudEndPointController.GetAllByParentId)]
        public async Task<IActionResult> GetAllByPropertyInfo(PropertyInformation parent)
        {
            var result = await _crudService.GetAllByPropertyInfo(parent);
            return Ok(result);
        }






        [HttpPost]
        [Route(CrudEndPointController.Create)]
        public virtual async Task<IActionResult> Create(TCreateViewModel viewModel)
        {
            await _crudService.CreateNew(viewModel);
            return Ok(viewModel);
        }

        [HttpDelete]
        [Route(CrudEndPointController.Delete)]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var viewModel = await _crudService.GetEntityAsViewModel(id, exceptionRaiseIfNotExist: true);
            await _crudService.Delete(id);
            return Ok(viewModel);
        }


        [HttpDelete]
        [Route(CrudEndPointController.SoftDelete)]
        public virtual async Task<IActionResult> SoftDelete(string id)
        {
            var viewModel = await _crudService.GetEntityAsViewModel(id, exceptionRaiseIfNotExist: true);
            await _crudService.SoftDelete(id);
            return Ok(viewModel);
        }


        [HttpPost]
        [Route(CrudEndPointController.Edit)]
        public virtual async Task<IActionResult> Edit(TEditViewModel viewModel)
        {
            await _crudService.EditEntity(viewModel);
            return Ok(viewModel);
        }

        [HttpGet]
        [Route(CrudEndPointController.GetAll)]
        public virtual async Task<IActionResult> GetAll(int? page = null, int perPage = Pagination.PerPage, string? search = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            await Task.CompletedTask;
            return RedirectToAction(nameof(base.PaginateAsViewModel), new
            {
                page = page
                ,
                perPage = perPage
                ,
                keyword = search
                ,
                startDate = startDate
                ,
                endDate = endDate
            });
        }

        [HttpGet]
        [Route(CrudEndPointController.GetAsViewModel)]
        public virtual async Task<IActionResult> GetAsViewModel(string id)
        {
            var result = await _crudService.GetEntityAsViewModel(id, exceptionRaiseIfNotExist: true);
            return Ok(result);
        }


        [HttpGet]
        [Route(CrudEndPointController.Get)]
        public virtual async Task<IActionResult> Get(string id)
        {
            var result = await _crudService.GetEntity(id, exceptionRaiseIfNotExist: true);
            return Ok(result);
        }

    }
}
