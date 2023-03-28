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

    [AttributeUsage(AttributeTargets.Class)]
    public class AvestaAPIControllerAttribute : ApiControllerAttribute
    {
    }

    [AvestaAPIController]
    public class CrudController<TId, TEntity, TModel, TCreateModel, TEditModel> : CrudController<TId, TEntity, TModel>
        , ICrudController<TId, TEntity, TModel, TCreateModel, TEditModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TCreateModel : TModel
        where TEditModel : TModel
    {
        readonly IEntityService<TId, TEntity, TModel, TCreateModel, TEditModel> _entityService;
        public CrudController(IEntityService<TId, TEntity, TModel, TCreateModel, TEditModel> entityService
            , IEntityService<TId, TEntity, TModel> entityService_) : base(entityService_)
        {
            _entityService = entityService;
        }

        [HttpPut]
        public virtual async Task<IActionResult> Create(TCreateModel model)
        {
            await _entityService.Insert(model);
            return base.Ok(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TEditModel model)
        {
            await _entityService.Update(model);
            return base.Ok(model);
        }

        [HttpPost]
        public Task<IActionResult> UpdateOrCreate(TEditModel model)
        {
            throw new NotImplementedException();
        }
    }


    [AvestaAPIController]
    public class CrudController<TId, TEntity, TModel> : AvestaAPIController, ICrudController<TId, TEntity, TModel>
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
        public virtual async Task<IActionResult> Create(TModel model)
        {
            await _entityService.Insert(model);
            return base.Ok(model);
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete(TId id)
        {
            await _entityService.Delete(id);
            return base.Ok(id);
        }

        [HttpDelete]
        public virtual async Task<IActionResult> SoftDelete(TId id)
        {
            await _entityService.SoftDelete(id);
            return base.Ok(id);
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(TId id)
        {
            var result = await _entityService.Get(id, includeAllPath: false, track: false, exceptionRaiseIfNotExist: true);
            return base.Ok(result);
        }

        [HttpGet]
        public virtual Task<IActionResult> GetAll(int? page = null, int? perPage = 7, string[] keyword = null)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TModel model)
        {
            await _entityService.Update(model, exceptionRaiseIfNotExist: true);
            return base.Ok(model);
        }

        [HttpPost]
        public Task<IActionResult> UpdateOrCreate(TModel model)
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
        Task<IActionResult> UpdateOrCreate(TModel model);
    }
    public interface IUpdateController<TId, TEntity, TModel, TEditModel> : IUpdateController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TEditModel : TModel
    {
        Task<IActionResult> Update(TEditModel model);
        Task<IActionResult> UpdateOrCreate(TEditModel model);
    }




    public interface IDeleteController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
        Task<IActionResult> Delete(TId id);
        Task<IActionResult> SoftDelete(TId id);
    }








    public class CrudController : AvestaAPIController
    {

    }

    public class AvestaAPIController : AvestaController
    {

    }




}
