using Avesta.Attribute.Controller;
using Avesta.Constant;
using Avesta.Data.Model;
using Avesta.Services;
using Avesta.Share.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Controller.API.Crud
{

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
        public virtual async Task<IActionResult> UpdateOrCreate(TEditModel model)
        {
            var any = await _entityService.Any(model);
            if (any)
                await _entityService.Update(model);
            else
                await _entityService.Insert(model);

            //TODO : modify wich action was effected - update or create

            return base.Ok(model);
        }
    }


    [AvestaAPIController]
    public class CrudController<TId, TEntity, TModel> : AvestaBaseAPIController, ICrudController<TId, TEntity, TModel>
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
        public virtual async Task<IActionResult> GetAll(int? page = null, int perPage = Pagination.PerPage, string[] keywords = null)
        {
            var result = await _entityService.GetAll(includeAllPath: false, page: page, perPage: perPage, keywords: keywords);
            return base.Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TModel model)
        {
            await _entityService.Update(model, exceptionRaiseIfNotExist: true);
            return base.Ok(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> UpdateOrCreate(TModel model)
        {
            var any = await _entityService.Any(model);
            if (any)
                await _entityService.Update(model);
            else
                await _entityService.Insert(model);

            //TODO : modify wich action was effected - update or create

            return base.Ok(model);
        }
    }


}
