using Avesta.Data.Context;
using Avesta.Data.Entity.Context;
using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Create
{
    public class CreateRepository<TEntity, TId, TContext> : BaseCreateRepository<TContext>, ICreateRepository<TEntity, TId>
        where TId : class
        where TContext : AvestaDbContext
        where TEntity : BaseEntity<TId>
    {
        public CreateRepository(TContext context) : base(context)
        {
        }

        public async Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities)
        {
            await base.ClearAllEntitiesThenAddRange<TEntity, TId>(insertEntities);
        }

        public async Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities)
        {
            await base.ClearRemoveListThenAddRange<TEntity, TId>(removeList, insertEntities);
        }

        public async Task Insert(TEntity entity)
        {
            await base.Insert<TEntity, TId>(entity);
        }

        public async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await base.InsertRange<TEntity, TId>(entities);
        }

        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
        {
            await base.ReCreate<TEntity, TId>(deleteCondition, insertEntities);
        }
    }

}
