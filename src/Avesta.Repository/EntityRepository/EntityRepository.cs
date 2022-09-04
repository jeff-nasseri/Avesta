using Avesta.Exceptions.Entity;
using Avesta.Exceptions.Reflection;
using Avesta.Model;
using Avesta.Model.Data;
using Avesta.Share.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Avesta.Repository.Entity
{

    public class EntityRepository<TEntity, TContext> : BaseRepository<TContext>, IRepository<TEntity>, IDisposable where TEntity : BaseEntity
        where TContext : DbContext
    {
        //effect lock for all queries in repository
        const bool IsLockReaderActive = true;

        readonly TContext _context;
        public EntityRepository(TContext context) : base(context)
        {
            _context = context;
        }

        #region entity state
        public async Task DetachEntity(TEntity entity)
        {
            await Task.CompletedTask;
            _context.Entry(entity).State = EntityState.Detached;
        }
        #endregion

        #region get entity
        public virtual async Task<TEntity> GetByIdAsync(int id, bool track = true, bool exceptionRaseIfNotExist = false)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null && exceptionRaseIfNotExist)
                throw new CanNotFoundEntityException(new int { }, null);

            if (!track)
                _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task<TEntity> GetByIdAsync(object key, bool track = true, bool exceptionRaseIfNotExist = false)
        {
            var entity = await _context.FindAsync<TEntity>(key);
            if (entity == null && exceptionRaseIfNotExist)
                throw new CanNotFoundEntityException(key);
            if (!track)
                _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> predicate, bool exceptionRaseIfNotExist = false)
        {
            var result = (await _context.Set<TEntity>().SingleOrDefaultAsync(predicate) ??
                (exceptionRaseIfNotExist ? throw new CanNotFoundEntityException(predicate) : default));
            return result;
        }
        public async Task<TEntity> GetEntity(string navigationPropertyPath, Expression<Func<TEntity, bool>> predicate, bool exceptionRaseIfNotExist = false)
        {
            var table = await Include(navigationPropertyPath);
            var entity = table.SingleOrDefault(predicate);

            if (entity == null && exceptionRaseIfNotExist)
                throw new CanNotFoundEntityException(predicate);

            return entity;
        }
        public async Task<TEntity> First(bool exceptionRaseIfNotExist)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync();
            if (exceptionRaseIfNotExist && entity == null)
                throw new CanNotFoundEntityException("first entity");
            return entity;
        }
        #endregion



        #region get entities

        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            _ = ids ?? throw new ArgumentNullException();

            var result = await ids.ForEach<int, TEntity>(async (id) =>
             {
                 var entity = await _context.Set<TEntity>().FindAsync(id);
                 return entity;
             });
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludeAllChildren(bool track = false)
        {
            var result = default(IEnumerable<TEntity>);
            var query = await Query(eager: true);
            if (track)
                result = query.AsNoTracking().ToList();
            else
                result = query.ToList();

            return result;

        }
        public async Task<TEntity> GetIncludeAllChildren(string id, bool track = false)
        {
            var result = default(TEntity);
            var query = await Query(eager: true);
            if (track)
                result = query.AsNoTracking().SingleOrDefault(e => e.ID == id);
            else
                result = query.SingleOrDefault(e => e.ID == id);

            if (result == null)
                throw new CanNotFoundEntityException(id);

            return result;

        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return (await _context.Set<TEntity>().ToListAsync());
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool track = false)
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
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool track = true)
        {
            var result =
                (
                track ? await _context.Set<TEntity>().Where(predicate).ToListAsync()
                : await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync()
                )
                ?? throw new ThereIsNoEntityWithCurrentPredicate(predicate.ToString());
            return result;
        }
        public async Task<IEnumerable<TEntity>> GetAllByInclude(string navigationPropertyPath)
        {
            var table = await Include(navigationPropertyPath);
            return table.ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(string navigationPropertyPath = null, Func<TEntity, TKey> descendingOrder = null)
        {
            var list = default(IEnumerable<TEntity>);
            if (!string.IsNullOrEmpty(navigationPropertyPath))
                list = await GetAllByInclude(navigationPropertyPath);
            if (descendingOrder != null)
                list = list.OrderByDescending(descendingOrder);
            return list;
        }
        #endregion


        #region availability
        public virtual async Task CheckAvailability(Expression<Func<TEntity, bool>> any)
        {
            _ = await _context.Set<TEntity>().AnyAsync(any)
                ? true : throw new CanNotFoundEntityException(any.ToString());
        }
        #endregion



        #region insert or update
        public virtual async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
        {
            var all = _context.Set<TEntity>().Where(deleteCondition).ToList();
            _context.Set<TEntity>().RemoveRange(all);
            await _context.Set<TEntity>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }
        public async Task ReCreate(string navigationPropertyPath, Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
        {
            var table = await Include(navigationPropertyPath);
            var all = table.Where(deleteCondition).ToList();
            _context.Set<TEntity>().RemoveRange(all);
            await _context.Set<TEntity>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAllTEntityInDbThenAddRange(IEnumerable<TEntity> insertEntities)
        {
            var all = _context.Set<TEntity>().ToList();
            _context.Set<TEntity>().RemoveRange(all);
            if (insertEntities != null && insertEntities.Count() != 0)
                await _context.Set<TEntity>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }
        public async Task ClearAllTEntityInDbThenAddRange(IEnumerable<TEntity> removeEntities, IEnumerable<TEntity> insertEntities)
        {
            var all = _context.Set<TEntity>().ToList();
            _context.Set<TEntity>().RemoveRange(removeEntities);
            if (insertEntities != null && insertEntities.Count() != 0)
                await _context.Set<TEntity>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
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
        public virtual async Task UpdateOrInsert(TEntity entity)
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
        }
        #endregion



        #region delete
        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsyncById(object id)
        {
            if (id == null)
                throw new ArgumentNullException("id can not be null");
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWithAllChildren(string id)
        {
            var query = await Query(eager: true);
            var entity = query.SingleOrDefault(e => e.ID == id);
            if (entity == null)
                throw new CanNotFoundEntityException(id);
            await DeleteAsync(entity);
        }

        public async Task SoftDelete(string id, bool exceptionRaseIfNotExist)
        {
            var entity = await GetByIdAsync(id, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
            entity.DeleteDate = DateTime.Now;
            await UpdateAsync(entity);
        }



        #endregion


        #region where
        public async Task<IEnumerable<TEntity>> WhereByInclude(string navigationPropertyPath, Expression<Func<TEntity, bool>> search)
        {
            var table = await Include(navigationPropertyPath);
            var result = await table.Where(search).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<TEntity>> GetAllByParentInfo(ParentInfo info)
        {
            await Task.CompletedTask;

            //typeof(TEntity).GetProperty(info.ParentIdFieldNameInChildClass) == null
            if (!typeof(TEntity).EnSureHasProperty(info.ParentIdFieldNameInChildClass))
                throw new CanNotFoundPropertyWithCurrentNameInCurrentType($"property : {info.ParentIdFieldNameInChildClass}, type name : {typeof(TEntity)}");


            Func<TEntity, bool> func = (entity) =>
             {
                 var parentIdInfo = entity.GetType().EnsureGetProperty(info.ParentIdFieldNameInChildClass);
                 if (parentIdInfo == null)
                     return false;

                 var parentIdValue = parentIdInfo.GetValue(entity).ToString();
                 if (parentIdValue == info.ParentId)
                     return true;

                 return false;
             };

            var result = _context.Set<TEntity>().Where(func).ToList();
            return result;
        }

        #endregion



        #region single
        public async Task<TEntity> SingleOrDefaultAsyncByInclude(string navigationPropertyPath, Expression<Func<TEntity, bool>> search)
        {
            var table = await Include(navigationPropertyPath);
            var result = await table.SingleOrDefaultAsync(search);
            return result;
        }
        public async Task<TEntity> SingleAsyncByInclude(string navigationPropertyPath, Expression<Func<TEntity, bool>> search)
        {
            var table = await Include(navigationPropertyPath);
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
        public async Task<IQueryable<TEntity>> Include(string navigationPropertyPath)
        {
            await Task.CompletedTask;
            var props = navigationPropertyPath.Split(";");
            var table = _context.Set<TEntity>() as IQueryable<TEntity>;
            foreach (var prop in props)
            {
                table = table.Include(prop);
            }
            return table;
        }

        public async Task<int> Count()
        {
            var count = await _context.Set<TEntity>().ToListAsync();
            return count.Count();
        }

        public async Task<bool> AnyByInclude(string navigationPropertyPath, Expression<Func<TEntity, bool>> any)
        {
            var table = await Include(navigationPropertyPath);
            var result = await table.AnyAsync(any);
            return result;
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> any)
        {
            var data = _context.Set<TEntity>();
            var result = await data.AnyAsync(any);
            return result;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> search, bool exceptionIfNotExist = false)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(search);
            if (entity == null && exceptionIfNotExist)
                throw new CanNotFoundEntityException(search);
            return entity;
        }


        public virtual async Task<IQueryable<TEntity>> Query(bool eager = false)
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
