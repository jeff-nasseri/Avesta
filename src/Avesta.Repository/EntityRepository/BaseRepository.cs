using Avesta.Data.Model;
using Avesta.Exceptions.Entity;
using Avesta.Exceptions.Reflection;
using Avesta.Share.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using Avesta.Share.Model;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Hosting;

namespace Avesta.Repository.EntityRepositoryRepository
{
    public abstract class BaseRepository<TContext, TIdType> : IDisposable
        where TIdType : class
        where TContext : DbContext
    {
        readonly TContext _context;
        public BaseRepository(TContext context)
        {
            _context = context;
        }
        #region entity state
        public async Task DetachEntity<TEntity>(TEntity entity)
            where TEntity : BaseEntity<TIdType>
        {
            await Task.CompletedTask;
            _context.Entry(entity).State = EntityState.Detached;
        }
        #endregion

        #region get entity
        public async Task<TEntity> GetById<TEntity>(object key, bool track = true, bool exceptionRaseIfNotExist = false)
            where TEntity : BaseEntity<TIdType>
        {
            var entity = await _context.FindAsync<TEntity>(key);
            if (entity == null && exceptionRaseIfNotExist)
                throw new CanNotFoundEntityException(key);
            if (!track)
                _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate, bool exceptionRaseIfNotExist = false)
            where TEntity : BaseEntity<TIdType>
        {
            var result = (await _context.Set<TEntity>().SingleOrDefaultAsync(predicate) ??
                (exceptionRaseIfNotExist ? throw new CanNotFoundEntityException(predicate) : default));
            return result;
        }
        public async Task<TEntity> GetEntity<TEntity>(string navigationPropertyPath, Expression<Func<TEntity, bool>> predicate, bool exceptionRaseIfNotExist = false)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var entity = table.SingleOrDefault(predicate);

            if (entity == null && exceptionRaseIfNotExist)
                throw new CanNotFoundEntityException(predicate);

            return entity;
        }
        public async Task<TEntity> First<TEntity>(bool exceptionRaseIfNotExist)
            where TEntity : BaseEntity<TIdType>
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync();
            if (exceptionRaseIfNotExist && entity == null)
                throw new CanNotFoundEntityException("first entity");
            return entity;
        }
        #endregion



        #region get entities

        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync<TEntity>(IEnumerable<int> ids)
            where TEntity : BaseEntity<TIdType>
        {
            _ = ids ?? throw new ArgumentNullException();

            var result = await ids.ForEach<int, TEntity>(async (id) =>
            {
                var entity = await _context.Set<TEntity>().FindAsync(id);
                return entity;
            });
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludeAllChildren<TEntity>(bool track = false)
            where TEntity : BaseEntity<TIdType>
        {
            var result = default(IEnumerable<TEntity>);
            var query = await Query<TEntity>(eager: true);
            if (track)
                result = query.AsNoTracking().ToList();
            else
                result = query.ToList();

            return result;
        }
        public async Task<IEnumerable<TEntity>> GetAllIncludeAllChildren<TEntity>(int skip, int take, bool track = false)
            where TEntity : BaseEntity<TIdType>
        {
            var result = default(IEnumerable<TEntity>);
            var query = await Query<TEntity>(eager: true);
            if (track)
                result = query.AsNoTracking().Skip(skip).Take(take).ToList();
            else
                result = query.Skip(skip).Take(take).ToList();

            return result;
        }
        public async Task<TEntity> GetIncludeAllChildren<TEntity>(string id, bool track = false)
            where TEntity : BaseEntity<TIdType>
        {
            var result = default(TEntity);
            var query = await Query<TEntity>(eager: true);
            if (track)
                result = query.AsNoTracking().SingleOrDefault(e => e.ID == id);
            else
                result = query.SingleOrDefault(e => e.ID == id);

            if (result == null)
                throw new CanNotFoundEntityException(id);

            return result;

        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
            where TEntity : BaseEntity<TIdType>
        {
            return (await _context.Set<TEntity>().ToListAsync());
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(int skip, int take)
            where TEntity : BaseEntity<TIdType>
        {
            return (await _context.Set<TEntity>().Skip(skip).Take(take).ToListAsync());
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(bool track = false)
            where TEntity : BaseEntity<TIdType>
        {
            List<TEntity> result = default;
            if (track)
            {
                result = await _context.Set<TEntity>().ToListAsync();
            }
            else
            {
                result = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            }
            return result;
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool track = true)
            where TEntity : BaseEntity<TIdType>
        {
            var result =
                (
                track ? await _context.Set<TEntity>().Where(predicate).ToListAsync()
                : await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync()
                )
                ?? throw new ThereIsNoEntityWithCurrentPredicate(predicate.ToString());
            return result;
        }
        public async Task<IEnumerable<TEntity>> GetAllByInclude<TEntity>(string navigationPropertyPath)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            return table.ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllByInclude<TEntity>(string navigationPropertyPath, int skip, int take)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            return table.Skip(skip).Take(take).ToList();
        }



        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity, TKey>(string navigationPropertyPath = null, Func<TEntity, TKey> descendingOrder = null)
            where TEntity : BaseEntity<TIdType>
        {
            var list = default(IEnumerable<TEntity>);
            if (!string.IsNullOrEmpty(navigationPropertyPath))
                list = await GetAllByInclude<TEntity>(navigationPropertyPath);
            if (descendingOrder != null)
                list = list.OrderByDescending(descendingOrder);
            return list;
        }
        #endregion


        #region availability
        public virtual async Task CheckAvailability<TEntity>(Expression<Func<TEntity, bool>> any)
            where TEntity : BaseEntity<TIdType>
        {
            _ = await _context.Set<TEntity>().AnyAsync(any)
                ? true : throw new CanNotFoundEntityException(any.ToString());
        }
        #endregion



        #region insert or update
        public virtual async Task InsertAsync<TEntity>(TEntity entity)
            where TEntity : BaseEntity<TIdType>
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task InsertRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : BaseEntity<TIdType>
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public async Task ReCreate<TEntity>(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
            where TEntity : BaseEntity<TIdType>
        {
            var all = _context.Set<TEntity>().Where(deleteCondition).ToList();
            _context.Set<TEntity>().RemoveRange(all);
            await _context.Set<TEntity>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }
        public async Task ReCreate<TEntity>(string navigationPropertyPath, Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var all = table.Where(deleteCondition).ToList();
            _context.Set<TEntity>().RemoveRange(all);
            await _context.Set<TEntity>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }



        public async Task ClearAllTEntityInDbThenAddRange<TEntity>(IEnumerable<TEntity> insertEntities)
            where TEntity : BaseEntity<TIdType>
        {
            var all = _context.Set<TEntity>().ToList();
            _context.Set<TEntity>().RemoveRange(all);
            if (insertEntities != null && insertEntities.Count() != 0)
                await _context.Set<TEntity>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }
        public async Task ClearAllTEntityInDbThenAddRange<TEntity>(IEnumerable<TEntity> removeEntities, IEnumerable<TEntity> insertEntities)
            where TEntity : BaseEntity<TIdType>
        {
            var all = _context.Set<TEntity>().ToList();
            _context.Set<TEntity>().RemoveRange(removeEntities);
            if (insertEntities != null && insertEntities.Count() != 0)
                await _context.Set<TEntity>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync<TEntity>(TEntity entity)
            where TEntity : BaseEntity<TIdType>
        {
            try
            {
                _context.Update(entity);
            }
            catch (System.InvalidOperationException)
            {
                var originalEntity = _context.Find(entity.GetType(), ((TEntity)entity).ID);
                _context.Entry(originalEntity).CurrentValues.SetValues(entity);
            }
            await _context.SaveChangesAsync();

        }
        public virtual async Task<TEntity> UpdateOrInsert<TEntity>(TEntity entity)
            where TEntity : BaseEntity<TIdType>
        {
            var en = await _context.Set<TEntity>().FindAsync(entity.ID);
            if (en == null)
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                await UpdateAsync(entity);
            }
            return entity;
        }
        #endregion



        #region delete
        public virtual async Task DeleteAsync<TEntity>(TEntity entity)
            where TEntity : BaseEntity<TIdType>
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsyncById<TEntity>(object id)
            where TEntity : BaseEntity<TIdType>
        {
            if (id == null)
                throw new ArgumentNullException("id can not be null");
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : BaseEntity<TIdType>
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWithAllChildren<TEntity>(string id)
            where TEntity : BaseEntity<TIdType>
        {
            var query = await Query<TEntity>(eager: true);
            var entity = query.SingleOrDefault(e => e.ID == id);
            if (entity == null)
                throw new CanNotFoundEntityException(id);
            await DeleteAsync(entity);
        }

        public async Task SoftDelete<TEntity>(string id, bool exceptionRaseIfNotExist)
            where TEntity : BaseEntity<TIdType>
        {
            var entity = await GetById<TEntity>(id, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
            entity.DeletedDate = DateTime.UtcNow;
            await UpdateAsync(entity);
        }



        #endregion


        #region where

        public async Task<IEnumerable<TResult>> DynamicQuery<TEntity, TResult>(string navigationPropertyPath, string where, string select, string orderBy, int skip, int take)
            where TEntity : BaseEntity<TIdType>
            where TResult : class
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var result = table.OrderBy(orderBy).Where(where).Select(select).Skip(skip).Take(take).ToDynamicList<TResult>();
            return result;
        }

        public async Task<int> Count<TEntity>(string navigationPropertyPath, string where)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var count = table.Count(where);
            return count;
        }

        public async Task<IEnumerable<TResult>> DynamicQuery<TEntity, TResult>(string navigationPropertyPath, string where, string select, string orderBy, int? takeFromLast)
            where TEntity : BaseEntity<TIdType>
            where TResult : class
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            int skip = default(int);
            var tableWhere = table.OrderBy(orderBy).Where(where);
            if (takeFromLast > 0 && tableWhere.Count() > takeFromLast)
            {
                skip = tableWhere.Count() - takeFromLast.Value;
            }
            var result = tableWhere.Select(select).Skip(skip).ToDynamicList<TResult>();
            return result;
        }



        public async Task<IEnumerable<TEntity>> WhereByInclude<TEntity>(string navigationPropertyPath, string dynamicQuery, int skip, int take)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var result = table.Where(dynamicQuery).Skip(skip).Take(take);
            return result;
        }
        public async Task<IEnumerable<TEntity>> WhereByInclude<TEntity>(string navigationPropertyPath, string dynamicQuery)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var result = table.Where(dynamicQuery);
            return result;
        }

        public async Task<IEnumerable<TEntity>> WhereByInclude<TEntity>(string navigationPropertyPath, Expression<Func<TEntity, bool>> search)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var result = await table.Where(search).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<TEntity>> GetAllByPropertyInfo<TEntity>(PropertyInformation info)
            where TEntity : BaseEntity<TIdType>
        {
            await Task.CompletedTask;

            //typeof(TEntity).GetProperty(info.ParentIdFieldNameInChildClass) == null
            if (!typeof(TEntity).EnSureHasProperty(info.PropertyName))
                throw new CanNotFoundPropertyWithCurrentNameInCurrentType($"property : {info.PropertyName}, type name : {typeof(TEntity)}");


            Func<TEntity, bool> func = (entity) =>
            {
                var parentIdInfo = entity.GetType().EnsureGetProperty(info.PropertyName);
                if (parentIdInfo == null)
                    return false;

                var parentIdValue = parentIdInfo.GetValue(entity).ToString();
                if (parentIdValue == info.PropertyValue)
                    return true;

                return false;
            };

            var result = _context.Set<TEntity>().Where(func).ToList();
            return result;
        }

        #endregion



        #region single
        public async Task<TEntity> SingleOrDefaultAsyncByInclude<TEntity>(string navigationPropertyPath, Expression<Func<TEntity, bool>> search)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var result = await table.SingleOrDefaultAsync(search);
            return result;
        }
        public async Task<TEntity> SingleAsyncByInclude<TEntity>(string navigationPropertyPath, Expression<Func<TEntity, bool>> search)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var result = await table.SingleOrDefaultAsync(search)
                ?? throw new CanNotFoundEntityException(search.ToString());
            return result;
        }

        #endregion



        #region include
        /// <summary>
        /// include dbset dynamiclly at runtime
        /// </summary>
        /// <param name="navigationPropertyPath">split props by ; like (entity;entity.sub)</param>
        /// <returns>IQueryable<TEntity></returns>
        public async Task<IQueryable<TEntity>> Include<TEntity>(string navigationPropertyPath)
            where TEntity : BaseEntity<TIdType>
        {
            await Task.CompletedTask;
            var table = _context.Set<TEntity>() as IQueryable<TEntity>;
            if (string.IsNullOrEmpty(navigationPropertyPath))
                return table;
            var props = navigationPropertyPath.Split(";");
            foreach (var prop in props)
            {
                table = table.Include(prop);
            }
            return table;
        }

        public async Task<int> Count<TEntity>()
            where TEntity : BaseEntity<TIdType>
        {
            var count = await _context.Set<TEntity>().ToListAsync();
            return count.Count();
        }

        public async Task<bool> AnyByInclude<TEntity>(string navigationPropertyPath, Expression<Func<TEntity, bool>> any)
            where TEntity : BaseEntity<TIdType>
        {
            var table = await Include<TEntity>(navigationPropertyPath);
            var result = await table.AnyAsync(any);
            return result;
        }

        public async Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> any)
            where TEntity : BaseEntity<TIdType>
        {
            var data = _context.Set<TEntity>();
            var result = await data.AnyAsync(any);
            return result;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> search, bool exceptionIfNotExist = false)
            where TEntity : BaseEntity<TIdType>
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(search);
            if (entity == null && exceptionIfNotExist)
                throw new CanNotFoundEntityException(search);
            return entity;
        }


        public virtual async Task<IQueryable<TEntity>> Query<TEntity>(bool eager = false)
            where TEntity : BaseEntity<TIdType>
        {
            await Task.CompletedTask;

            var query = _context.Set<TEntity>().AsQueryable();
            if (eager)
            {
                var navigations = _context.Model.FindEntityType(typeof(TEntity))
                    .GetDerivedTypesInclusive()
                    .SelectMany(type => type.GetNavigations())
                    .Distinct();

                foreach (var property in navigations)
                    query = query.Include(property.Name);
            }
            return query;
        }

        #endregion







    }




}
