


using AutoMapper;
using Avesta.Share.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Avesta.Repository.EntityRepository;
using Avesta.Storage.Constant;
using Avesta.Data.Model;
using Avesta.Share.Model;
using Avesta.Share.Model.Controller;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http;
using Avesta.Services.Create;
using Avesta.Services.Delete;
using Avesta.Services.Graph;
using Avesta.Services.Read;
using Avesta.Services.Update;
using MoreLinq;

namespace Avesta.Services
{
    public class EntityService<TId, TEntity, TModel, TCreateModel, TEditModel> : EntityService<TId, TEntity, TModel>, IEntityService<TId, TEntity, TModel, TCreateModel, TEditModel>
       where TId : class
       where TEntity : BaseEntity<TId>
       where TModel : BaseModel<TId>
       where TCreateModel : TModel
       where TEditModel : TModel
    {

        readonly ICreateEntityService<TId, TEntity, TModel, TCreateModel> _createEntityService;
        readonly IUpdateEntityService<TId, TEntity, TModel, TEditModel> _updateEntityService;

        public EntityService(IReadEntityService<TId, TEntity, TModel> readEntityService
            , IUpdateEntityService<TId, TEntity, TModel, TEditModel> updateEntityService
            , IDeleteEntityService<TId, TEntity, TModel> deleteEntityService
            , IEntityGraphService<TId, TEntity, TModel> entityGraphService
            , ICreateEntityService<TId, TEntity, TModel, TCreateModel> createEntityService)
            : base(readEntityService
                , updateEntityService
                , deleteEntityService
                , entityGraphService
                , createEntityService)
        {
            _createEntityService = createEntityService;
            _updateEntityService = updateEntityService;
        }

        public async Task ClearAllEntitiesThenAddRange(IEnumerable<TCreateModel> insertModels)
            => await _createEntityService.ClearAllEntitiesThenAddRange(insertModels);

        public async Task ClearRemoveListThenAddRange(IEnumerable<TModel> removeList, IEnumerable<TCreateModel> insertModels)
            => await _createEntityService.ClearRemoveListThenAddRange(removeList, insertModels);

        public async Task Insert(TCreateModel model)
            => await _createEntityService.Insert(model);

        public async Task InsertRange(IEnumerable<TCreateModel> models)
            => await _createEntityService.InsertRange(models);

        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TCreateModel> insertModels)
            => await _createEntityService.ReCreate(deleteCondition, insertModels);

        public async Task Update(TEditModel model, bool exceptionRaiseIfNotExist = false)
            => await _updateEntityService.Update(model, exceptionRaiseIfNotExist);
    }


    public class EntityService<TId, TEntity, TModel> : IEntityService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {

        readonly IReadEntityService<TId, TEntity, TModel> _readEntityService;
        readonly IUpdateEntityService<TId, TEntity, TModel> _updateEntityService;
        readonly IDeleteEntityService<TId, TEntity, TModel> _deleteEntityService;
        readonly IEntityGraphService<TId, TEntity, TModel> _entityGraphService;
        readonly ICreateEntityService<TId, TEntity, TModel> _createEntityService;

        public EntityService(IReadEntityService<TId, TEntity, TModel> readEntityService
            , IUpdateEntityService<TId, TEntity, TModel> updateEntityService
            , IDeleteEntityService<TId, TEntity, TModel> deleteEntityService
            , IEntityGraphService<TId, TEntity, TModel> entityGraphService
            , ICreateEntityService<TId, TEntity, TModel> createEntityService)
        {
            _readEntityService = readEntityService;
            _updateEntityService = updateEntityService;
            _deleteEntityService = deleteEntityService;
            _entityGraphService = entityGraphService;
            _createEntityService = createEntityService;
        }


        #region [- Read -]
        public async Task<TModel> First(string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.First(navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TModel> First(bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.First(includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TModel> First(IQueryable<TEntity> entities
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.First(entities, track, exceptionRaiseIfNotExist);

        public async Task<TModel> First(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.First(search, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TModel> First(Expression<Func<TEntity, bool>> search
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.First(search, includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TModel> First(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.First(entities, search, track, exceptionRaiseIfNotExist);

        public async Task<TModel> Get(TId key
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.Get(key, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TModel> Get(TId key
            , bool includeAllPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.Get(key, includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TModel> Get(IQueryable<TEntity> entities
            , TId key
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.Get(entities, key, track, exceptionRaiseIfNotExist);

        public async Task<TModel> Get(Expression<Func<TEntity, bool>> predicate
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.Get(predicate, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TModel> Get(Expression<Func<TEntity, bool>> predicate
            , bool includeAllPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.Get(predicate, includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TModel> Get(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> predicate
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            => await _readEntityService.Get(entities, predicate, track, exceptionRaiseIfNotExist);

        public async Task<IEnumerable<TModel>> GetAll<TKey>(string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.GetAll(navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TModel>> GetAll<TKey>(bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.GetAll(includeAllPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TModel>> GetAll<TKey>(IQueryable<TEntity> entities
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.GetAll(entities, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TModel>> GetByIds<TKey>(IEnumerable<TId> ids
            , string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.GetByIds(ids, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TModel>> GetByIds<TKey>(IEnumerable<TId> ids
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.GetByIds(ids, includeAllPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TModel>> GetByIds<TKey>(IQueryable<TEntity> entities
            , IEnumerable<TId> ids
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.GetByIds(entities, ids, page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TModel>> Where<TKey>(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.Where(search, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TModel>> Where<TKey>(Expression<Func<TEntity, bool>> search
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.Where(search, includeAllPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TModel>> Where<TKey>(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            => await _readEntityService.Where(entities, search, page, perPage, orderBy, orderbyDirection, track);


        public async Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath)
            => await _readEntityService.Count(where, navigationPropertyPath);

        public async Task<int> Count(Expression<Func<TEntity, bool>> where)
            => await _readEntityService.Count(where);

        public async Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
            => await _readEntityService.Count(entities, where);

        public async Task<int> Count()
            => await _readEntityService.Count();


        #endregion


        #region [- Create -]
        public async Task ClearAllEntitiesThenAddRange(IEnumerable<TModel> insertModels)
            => await _createEntityService.ClearAllEntitiesThenAddRange(insertModels);

        public async Task ClearRemoveListThenAddRange(IEnumerable<TModel> removeList, IEnumerable<TModel> insertModels)
            => await _createEntityService.ClearRemoveListThenAddRange(removeList, insertModels);

        public async Task Insert(TModel model)
            => await _createEntityService.Insert(model);

        public async Task InsertRange(IEnumerable<TModel> models)
            => await _createEntityService.InsertRange(models);

        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TModel> insertModels)
            => await _createEntityService.ReCreate(deleteCondition, insertModels);
        #endregion


        #region [- Delete -]
        public async Task Delete(TModel model, bool exceptionRaiseIfNotExist = false)
            => await _deleteEntityService.Delete(model, exceptionRaiseIfNotExist);

        public async Task Delete(TId id, bool exceptionRaiseIfNotExist = false)
            => await _deleteEntityService.Delete(id, exceptionRaiseIfNotExist);

        public async Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false)
            => await _deleteEntityService.Delete(single, exceptionRaiseIfNotExist);

        public async Task DeleteRange(IEnumerable<TModel> models)
            => await _deleteEntityService.DeleteRange(models);

        public async Task DeleteRange(Expression<Func<TEntity, bool>> where)
            => await _deleteEntityService.DeleteRange(where);


        public async Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false)
            => await _deleteEntityService.SoftDelete(id, exceptionRaiseIfNotExist);

        #endregion


        #region [- Graph -]
        public async Task<IEnumerable<TModel>> GraphQuery(string navigationPropertyPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
            => await _entityGraphService.GraphQuery(navigationPropertyPath, where, select, orderBy, page, perPage);

        public async Task<IEnumerable<TModel>> GraphQuery(string includeAllPath
            , bool where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
            => await _entityGraphService.GraphQuery(includeAllPath, where, select, orderBy, page, perPage);

        public async Task<IEnumerable<TModel>> GraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
            => await _entityGraphService.GraphQuery(entities, where, select, orderBy, page, perPage);
        #endregion


        #region [- Update -]
        public async Task Update(TModel model, bool exceptionRaiseIfNotExist = false)
            => await _updateEntityService.Update(model, exceptionRaiseIfNotExist);
        #endregion

    }



}
