using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.EntityRepository.Avability;
using Avesta.Repository.EntityRepository.Create;
using Avesta.Repository.EntityRepository.Delete;
using Avesta.Repository.EntityRepository.Read;
using Avesta.Repository.EntityRepository.Update;
using Avesta.Share.Model;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
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
        where TContext : AvestaDbContext
    {
        readonly IReadRepository<TEntity, TId> _readRepository;
        readonly ICreateRepository<TEntity, TId> _createRepository;
        readonly IDeleteRepository<TEntity, TId> _deleteRepository;
        readonly IAvailabilityRepository<TEntity, TId> _availabilityRepository;
        readonly IUpdateRepository<TEntity, TId> _updateRepository;


        public EntityRepository(IReadRepository<TEntity, TId> readRepository
            , ICreateRepository<TEntity, TId> createRepository
            , IDeleteRepository<TEntity, TId> deleteRepository
            , IAvailabilityRepository<TEntity, TId> availabilityRepository
            , IUpdateRepository<TEntity, TId> updateRepository
            , TContext context)
        {
            _readRepository = readRepository;
            _createRepository = createRepository;
            _deleteRepository = deleteRepository;
            _availabilityRepository = availabilityRepository;
            _updateRepository = updateRepository;
        }



        #region [- Availability -]
        public async Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath = null)
        {
            var result = await _availabilityRepository.Any(any, navigationPropertyPath);
            return result;
        }

        public async Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath = null)
        {
            await _availabilityRepository.CheckAvailability(any, navigationPropertyPath);
        }
        #endregion



        #region [- Create -]
        public async Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities)
        {
            await _createRepository.ClearAllEntitiesThenAddRange(insertEntities);
        }
        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
        {
            await _createRepository.ReCreate(deleteCondition, insertEntities);
        }
        public async Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities)
        {
            await _createRepository.ClearRemoveListThenAddRange(removeList, insertEntities);
        }
        public async Task Insert(TEntity entity)
        {
            await _createRepository.Insert(entity);
        }

        public async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await _createRepository.InsertRange(entities);
        }

        #endregion



        #region [- Delete -]
        public async Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false)
        {
            await _deleteRepository.Delete(entity, exceptionRaiseIfNotExist);
        }


        public async Task SoftDelete(string id, bool exceptionRaiseIfNotExist = false)
        {
            await _deleteRepository.SoftDelete(id, exceptionRaiseIfNotExist);
        }

        public async Task Delete(object id, bool exceptionRaiseIfNotExist = false)
        {
            await _deleteRepository.Delete(id, exceptionRaiseIfNotExist);
        }

        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            await _deleteRepository.DeleteRange(entities);
        }

        public async Task DeleteWithAllChildren(string id, bool exceptionRaiseIfNotExist = false)
        {
            await _deleteRepository.DeleteWithAllChildren(id, exceptionRaiseIfNotExist);
        }
        #endregion

        

        #region [- Read -]
        public async Task<TEntity> FirstOrDefault(string navigationPropertyPath = null
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var result = await _readRepository.FirstOrDefault(navigationPropertyPath, includeAllPath, track, exceptionRaiseIfNotExist);
            return result;
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath = null
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var result = await _readRepository.FirstOrDefault(search, navigationPropertyPath, includeAllPath, track, exceptionRaiseIfNotExist);
            return result;
        }

        public async Task<TEntity> Get(object key
            , string navigationPropertyPath = null
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var result = await _readRepository.Get(key, navigationPropertyPath, includeAllPath, track, exceptionRaiseIfNotExist);
            return result;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
            , string navigationPropertyPath = null
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
        {
            var result = await _readRepository.Get(predicate, navigationPropertyPath, includeAllPath, track, exceptionRaiseIfNotExist);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll<TKey>(int? page = null
            , int perPage = 7
            , string navigationPropertyPath = null
            , bool includeAllPath = false
            , Expression<Func<TEntity, TKey>> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var result = await _readRepository.GetAll<TKey>(page, perPage, navigationPropertyPath, includeAllPath, orderBy, orderbyDirection, track);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll(int? page = null
            , int perPage = 7
            , string navigationPropertyPath = null
            , bool includeAllPath = false
            , Expression<Func<TEntity, object>> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var result = await _readRepository.GetAll(page, perPage, navigationPropertyPath, includeAllPath, orderBy, orderbyDirection, track);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<int> ids
            , int? page = null
            , int perPage = 7
            , string navigationPropertyPath = null
            , bool includeAllPath = false
            , Expression<Func<TEntity, TKey>> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var result = await _readRepository.GetByIds<TKey>(ids, page, perPage, navigationPropertyPath, includeAllPath, orderBy, orderbyDirection, track);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<int> ids
            , int? page = null
            , int perPage = 7
            , string navigationPropertyPath = null
            , bool includeAllPath = false
            , Expression<Func<TEntity, object>> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var result = await _readRepository.GetByIds(ids, page, perPage, navigationPropertyPath, includeAllPath, orderBy, orderbyDirection, track);
            return result;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> search
           , int? page = null
           , int perPage = 7
           , string navigationPropertyPath = null
           , bool includeAllPath = false
           , Expression<Func<TEntity, object>> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending, bool track = false)
        {
            var result = await _readRepository.Where(search, page, perPage, navigationPropertyPath, includeAllPath, orderBy, orderbyDirection, track);
            return result;
        }

        public async Task<IEnumerable<TEntity>> Where(string dynamicQuery
            , int? page = null
            , int perPage = 7
            , string navigationPropertyPath = null
            , bool includeAllPath = false
            , Expression<Func<TEntity, object>> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
        {
            var result = await _readRepository.Where(dynamicQuery, page, perPage, navigationPropertyPath, includeAllPath, orderBy, orderbyDirection, track);
            return result;
        }
        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> search
       , string navigationPropertyPath = null
       , bool includeAllPath = false
       , bool track = true
       , bool exceptionRaiseIfNotExist = false)
        {
            var result = await _readRepository.SingleOrDefault(search, navigationPropertyPath, includeAllPath, track, exceptionRaiseIfNotExist);
            return result;
        }

        #endregion

        
        #region [- Update -]
        public async Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false)
        {
            await _updateRepository.Update(entity, exceptionRaiseIfNotExist);
        }
        #endregion



        public Task<IQueryable<TEntity>> Include(string navigationPropertyPath)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<TResult>> QraphQuery<TResult>(string where, string select, string orderBy, string navigationPropertyPath = null, bool includeAllPath = false, int? page = null, int perPage = 7, bool track = false) where TResult : class
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> Query(bool eager = false)
        {
            throw new NotImplementedException();
        }
        public Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }


    }

}
