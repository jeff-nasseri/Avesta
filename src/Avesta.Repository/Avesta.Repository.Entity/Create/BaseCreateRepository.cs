
using Avesta.Data.Entity.Context;
using Avesta.Data.Entity.Model;
using Avesta.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Create
{
    public class BaseCreateRepository<TContext> : BaseRepository<TContext>
        where TContext : AvestaDbContext
    {
        readonly TContext _context;
        public BaseCreateRepository(TContext context) : base(context)
        {
            _context = context;
        }
        public async Task ClearAllEntitiesThenAddRange<TEntity, TId>(IEnumerable<TEntity> insertEntities)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var all = base.Table<TEntity, TId>().ToList();
            base.Table<TEntity, TId>().RemoveRange(all);
            if (insertEntities != null && insertEntities.Count() != 0)
                await base.Table<TEntity, TId>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }






        public async Task ClearRemoveListThenAddRange<TEntity, TId>(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var all = base.Table<TEntity, TId>().ToList();
            base.Table<TEntity, TId>().RemoveRange(removeList);
            if (insertEntities != null && insertEntities.Count() != 0)
                await base.Table<TEntity, TId>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }







        public async Task Insert<TEntity, TId>(TEntity entity)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            await base.Table<TEntity, TId>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }






        public async Task InsertRange<TEntity, TId>(IEnumerable<TEntity> entities)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            await base.Table<TEntity, TId>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }






        public async Task ReCreate<TEntity,TId>(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var all = base.Table<TEntity, TId>().Where(deleteCondition).ToList();
            base.Table<TEntity, TId>().RemoveRange(all);
            await base.Table<TEntity, TId>().AddRangeAsync(insertEntities);
            await _context.SaveChangesAsync();
        }


    }

}
