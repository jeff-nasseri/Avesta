using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
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

    public class EntityRepository<TEntity, TContext, TIdType> : BaseRepository<TContext, TIdType>, IRepository<TEntity>
        where TIdType : class
        where TEntity : BaseEntity<TIdType>
        where TContext : DbContext
    {
        readonly TContext _context;
        public EntityRepository(TContext context) : base(context)
        {
            _context = context;
        }

        #region entity state
        public async Task DetachEntity(TEntity entity)
        {
            await base.DetachEntity<TEntity>(entity);
        }
        #endregion

        #region [- Read -]
        public async Task<TEntity> GetById(object key, bool track = true, bool exceptionRaiseIfNotExist = false)
        {
            return await base.GetById<TEntity>(key, track, exceptionRaiseIfNotExist);
        }
        public async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> predicate, bool track = true, bool exceptionRaiseIfNotExist = false)
        {
            return await base.GetEntity<TEntity>(predicate, track, exceptionRaiseIfNotExist);
        }
        public async Task<TEntity> GetEntity(string navigationPropertyPath, Expression<Func<TEntity, bool>> predicate, bool track = true, bool exceptionRaiseIfNotExist = false)
        {
            return await base.GetEntity<TEntity>(navigationPropertyPath, predicate, track, exceptionRaiseIfNotExist);
        }
        public async Task<TEntity> FirstOrDefault(bool track = true, bool exceptionRaiseIfNotExist = false)
        {
            return await base.FirstOrDefault<TEntity>(track: track, exceptionRaiseIfNotExist: exceptionRaiseIfNotExist);
        }
        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> search, bool track = true, bool exceptionRaiseIfNotExist = false)
        {
            return await base.FirstOrDefault<TEntity>(search, track: track, exceptionRaiseIfNotExist: exceptionRaiseIfNotExist);
        }

        public async Task<TEntity> SingleOrDefault(string navigationPropertyPath, Expression<Func<TEntity, bool>> search, bool track = true, bool exceptionRaiseIfNotExist = false)
        {
            return await base.SingleOrDefault<TEntity>(navigationPropertyPath, search, track: track, exceptionRaiseIfNotExist: exceptionRaiseIfNotExist);
        }
        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> search, bool track = true, bool exceptionRaiseIfNotExist = false)
        {
            return await base.SingleOrDefault<TEntity>(search, track: track, exceptionRaiseIfNotExist: exceptionRaiseIfNotExist);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await base.GetByIds<TEntity>(ids);
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludeAllChildren(bool track = true)
        {
            return await base.GetAllIncludeAllChildren<TEntity>(track);
        }
        public async Task<IEnumerable<TEntity>> GetAllIncludeAllChildren<TKey>(int skip, int take, Expression<Func<TEntity, TKey>> orderBy = null, OrderByDirection orderbyDirection = OrderByDirection.Ascending, bool track = false)
        {
            return await base.GetAllIncludeAllChildren<TEntity, TKey>(skip, take, orderBy, orderbyDirection, track);

        }
        public async Task<TEntity> GetIncludeAllChildren(string id, bool track = false)
        {
            return await base.GetIncludeAllChildren<TEntity>(id, track);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await base.GetAll<TEntity>();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take)
        {
            return await base.GetAll<TEntity>(skip, take);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool track = false)
        {
            return await base.GetAllAsync<TEntity>(track);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool track = true)
        {
            return await base.GetAll<TEntity>(predicate, track);
        }
        public async Task<IEnumerable<TEntity>> GetAllByInclude(string navigationPropertyPath)
        {
            return await base.GetAllByInclude<TEntity>(navigationPropertyPath);
        }
        public async Task<IEnumerable<TEntity>> GetAllByInclude(string navigationPropertyPath, int skip, int take)
        {
            return await base.GetAllByInclude<TEntity>(navigationPropertyPath, skip, take);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(string navigationPropertyPath = null, Func<TEntity, TKey> descendingOrder = null)
        {
            return await base.GetAll<TEntity, TKey>(navigationPropertyPath, descendingOrder);
        }

        #endregion



        #region availability
        public virtual async Task CheckAvailability(Expression<Func<TEntity, bool>> any)
        {
            await base.CheckAvailability<TEntity>(any);
        }
        #endregion



        #region insert or update
        public virtual async Task InsertAsync(TEntity entity)
        {
            await base.InsertAsync<TEntity>(entity);
        }
        public async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await base.InsertRange<TEntity>(entities);
        }
        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
        {
            await base.ReCreate<TEntity>(deleteCondition, insertEntities);
        }
        public async Task ReCreate(string navigationPropertyPath, Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
        {
            await base.ReCreate<TEntity>(navigationPropertyPath, deleteCondition, insertEntities);
        }

        public async Task ClearAllTEntityInDbThenAddRange(IEnumerable<TEntity> insertEntities)
        {
            await base.ClearAllTEntityInDbThenAddRange<TEntity>(insertEntities);
        }
        public async Task ClearAllTEntityInDbThenAddRange(IEnumerable<TEntity> removeEntities, IEnumerable<TEntity> insertEntities)
        {
            await base.ClearAllTEntityInDbThenAddRange<TEntity>(removeEntities, insertEntities);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await base.UpdateAsync<TEntity>(entity);
        }
        public virtual async Task<TEntity> UpdateOrInsert(TEntity entity)
        {
            var result = await base.UpdateOrInsert<TEntity>(entity);
            return result;
        }
        #endregion



        #region delete
        public virtual async Task DeleteAsync(TEntity entity)
        {
            await base.DeleteAsync<TEntity>(entity);
        }
        public async Task DeleteAsyncById(object id)
        {
            await base.DeleteAsyncById<TEntity>(id);
        }
        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            await base.DeleteRangeAsync<TEntity>(entities);
        }

        public async Task DeleteWithAllChildren(string id)
        {
            await base.DeleteWithAllChildren<TEntity>(id);
        }

        public async Task SoftDelete(string id, bool exceptionRaiseIfNotExist)
        {
            await base.SoftDelete<TEntity>(id, exceptionRaiseIfNotExist);
        }



        #endregion


        #region where
        public async Task<IEnumerable<TEntity>> WhereByInclude(string navigationPropertyPath, Expression<Func<TEntity, bool>> search)
        {
            return await base.WhereByInclude<TEntity>(navigationPropertyPath, search);
        }
        public async Task<IEnumerable<TEntity>> WhereByInclude(string navigationPropertyPath, string dynamicQuery, int skip, int take)
        {
            return await base.WhereByInclude<TEntity>(navigationPropertyPath, dynamicQuery, skip: skip, take: take);
        }

        public async Task<IEnumerable<TResult>> DynamicQuery<TResult>(string navigationPropertyPath, string where, string select, string orderBy, int skip, int take)
            where TResult : class
        {
            return await base.DynamicQuery<TEntity, TResult>(navigationPropertyPath, where, select, orderBy: orderBy, skip: skip, take: take);
        }

        public async Task<IEnumerable<TResult>> DynamicQuery<TResult>(string navigationPropertyPath, string where, string select, string orderBy, int? takeFromLast)
            where TResult : class
        {
            return await base.DynamicQuery<TEntity, TResult>(navigationPropertyPath, where, select, orderBy, takeFromLast);
        }


        public async Task<IEnumerable<TEntity>> WhereByInclude(string navigationPropertyPath, string dynamicQuery)
        {
            return await base.WhereByInclude<TEntity>(navigationPropertyPath, dynamicQuery);
        }

        public async Task<IEnumerable<TEntity>> GetAllByPropertyInfo(PropertyInformation info)
        {
            return await base.GetAllByPropertyInfo<TEntity>(info);
        }

        #endregion





        #region include
        /// <summary>
        /// include dbset dynamiclly at runtime
        /// </summary>
        /// <param name="navigationPropertyPath">split props by ; like (entity;entity.sub)</param>
        /// <returns>IQueryable<TEntity></returns>
        public async Task<IQueryable<TEntity>> Include(string navigationPropertyPath)
        {
            return await base.Include<TEntity>(navigationPropertyPath);
        }

        public async Task<int> Count()
        {
            return await base.Count<TEntity>();
        }

        public async Task<bool> AnyByInclude(string navigationPropertyPath, Expression<Func<TEntity, bool>> any)
        {
            return await base.AnyByInclude<TEntity>(navigationPropertyPath, any);
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> any)
        {
            return await base.Any(any);
        }




        public virtual async Task<IQueryable<TEntity>> Query(bool eager = false)
        {
            return await base.Query<TEntity>(eager);
        }

        public async Task<int> Count(string navigationPropertyPath, string where)
        {
            return await base.Count<TEntity>(navigationPropertyPath, where);
        }

        #endregion





        #region Properties

        public IQueryable<TEntity> Table
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        #endregion
    }

}
