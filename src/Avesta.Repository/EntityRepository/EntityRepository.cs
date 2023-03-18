using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.EntityRepository.Avability;
using Avesta.Repository.EntityRepository.Create;
using Avesta.Repository.EntityRepository.Delete;
using Avesta.Repository.EntityRepository.Qraph;
using Avesta.Repository.EntityRepository.Read;
using Avesta.Repository.EntityRepository.Update;
using Avesta.Share.Model;
using Avesta.Storage.Constant;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using MoreLinq.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepositoryRepository
{

    public class EntityRepository<TEntity, TContext, TId> : IRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        readonly IReadRepository<TEntity, TId> _readRepository;
        readonly ICreateRepository<TEntity, TId> _createRepository;
        readonly IDeleteRepository<TEntity, TId> _deleteRepository;
        readonly IAvailabilityRepository<TEntity, TId> _availabilityRepository;
        readonly IGraphRepository<TEntity, TId> _graphRepository;
        readonly IUpdateRepository<TEntity, TId> _updateRepository;


        public EntityRepository(IReadRepository<TEntity, TId> readRepository
            , ICreateRepository<TEntity, TId> createRepository
            , IDeleteRepository<TEntity, TId> deleteRepository
            , IAvailabilityRepository<TEntity, TId> availabilityRepository
            , IGraphRepository<TEntity, TId> graphRepository
            , IUpdateRepository<TEntity, TId> updateRepository)
        {
            _readRepository = readRepository;
            _createRepository = createRepository;
            _deleteRepository = deleteRepository;
            _availabilityRepository = availabilityRepository;
            _graphRepository = graphRepository;
            _updateRepository = updateRepository;
        }





        #region [- Graph -]
        public async Task<IEnumerable<TEntity>> GraphQuery(string navigationPropertyPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
                => await _graphRepository.GraphQuery(navigationPropertyPath, where, select, orderBy, page, perPage, track);

        public async Task<IEnumerable<TEntity>> GraphQuery(string includeAllPath
            , bool where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
                => await _graphRepository.GraphQuery(includeAllPath, where, select, orderBy, page, perPage, track);

        public async Task<IEnumerable<TEntity>> GraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
                => await _graphRepository.GraphQuery(entities, where, select, orderBy, page, perPage, track);
        #endregion



        #region [- Availability -]
        public async Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
            => await _availabilityRepository.Any(any, navigationPropertyPath);

        public async Task<bool> Any(Expression<Func<TEntity, bool>> any)
            => await _availabilityRepository.Any(any);

        public async Task<bool> Any(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
            => await _availabilityRepository.Any(entities, any);



        public async Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
            => await _availabilityRepository.CheckAvailability(any, navigationPropertyPath);

        public async Task CheckAvailability(Expression<Func<TEntity, bool>> any)
            => await _availabilityRepository.CheckAvailability(any);

        public async Task CheckAvailability(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
            => await _availabilityRepository.CheckAvailability(entities, any);
        #endregion



        #region [- Create -]

        public async Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities)
            => await _createRepository.ClearAllEntitiesThenAddRange(insertEntities);

        public async Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities)
            => await _createRepository.ClearRemoveListThenAddRange(removeList, insertEntities);
        public async Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false)
            => await _deleteRepository.SoftDelete(id, exceptionRaiseIfNotExist);


        public async Task Insert(TEntity entity)
             => await _createRepository.Insert(entity);

        public async Task InsertRange(IEnumerable<TEntity> entities)
            => await _createRepository.InsertRange(entities);

        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
            => await _createRepository.ReCreate(deleteCondition, insertEntities);

        #endregion



        #region [- Delete -]
        public async Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false)
            => await _deleteRepository.Delete(entity, exceptionRaiseIfNotExist);

        public async Task Delete(TId id, bool exceptionRaiseIfNotExist = false)
            => await _deleteRepository.Delete(id, exceptionRaiseIfNotExist);

        public async Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false)
            => await _deleteRepository.Delete(single, exceptionRaiseIfNotExist);

        public async Task DeleteRange(IEnumerable<TEntity> entities)
            => await _deleteRepository.DeleteRange(entities);

        public async Task DeleteRange(Expression<Func<TEntity, bool>> where)
            => await _deleteRepository.DeleteRange(where);
        #endregion



        #region [- Read -]
        public async Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.Where(search, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.Where(search, includeAllPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> Where<TKey>(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.Where(entities, search, page, perPage, orderBy, orderbyDirection, track);
        public async Task<TEntity> Get(TId key, string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.Get(key, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get(TId key, bool includeAllPath, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.Get(key, includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get(IQueryable<TEntity> entities, TId key, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.Get(entities, key, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.Get(predicate, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool includeAllPath, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.Get(predicate, includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> predicate, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.Get(entities, predicate, track, exceptionRaiseIfNotExist);
        public async Task<TEntity> First(string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.First(navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(bool includeAllPath = false, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.First(includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(IQueryable<TEntity> entities, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.First(entities, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(Expression<Func<TEntity, bool>> search, string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.First(search, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(Expression<Func<TEntity, bool>> search, bool includeAllPath = false, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.First(search, includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> search, bool track = true, bool exceptionRaiseIfNotExist = false)
            => await _readRepository.First(entities, search, track, exceptionRaiseIfNotExist);


        public async Task<IEnumerable<TEntity>> GetAll<TKey>(string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.GetAll(navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> GetAll<TKey>(bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.GetAll(includeAllPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> GetAll<TKey>(IQueryable<TEntity> entities
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.GetAll(entities, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
            , string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.GetByIds(ids, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.GetByIds(ids, includeAllPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IQueryable<TEntity> entities
            , IEnumerable<TId> ids
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await _readRepository.GetByIds(entities, ids, page, perPage, orderBy, orderbyDirection, track);


   
        public async Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath = null)
            => await _readRepository.Count(where, navigationPropertyPath);

        public async Task<int> Count(Expression<Func<TEntity, bool>> where)
            => await _readRepository.Count(where);

        public async Task<int> Count()
            => await _readRepository.Count();
        public async Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
            => await _readRepository.Count(entities, where);

        #endregion


        #region [- Update -]

        public async Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false)
           => await _updateRepository.Update(entity, exceptionRaiseIfNotExist);

        #endregion




    }

}
