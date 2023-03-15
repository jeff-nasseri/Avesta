using Avesta.Data.Context;
using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Delete
{
    public class DeleteRepository<TEntity, TId, TContext> : BaseDeleteRepository<TContext>, IDeleteRepository<TEntity, TId>
        where TId : class
        where TContext : AvestaDbContext
        where TEntity : BaseEntity<TId>
    {
        public DeleteRepository(TContext context) : base(context)
        {
        }

        public async Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false)
        {
            await base.Delete<TEntity, TId>(entity, exceptionRaiseIfNotExist);
        }

        public async Task Delete(TId id, bool exceptionRaiseIfNotExist = false)
        {
            await base.Delete<TEntity, TId>(id, exceptionRaiseIfNotExist);
        }

        public async Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false)
        {
            await base.Delete<TEntity, TId>(single, exceptionRaiseIfNotExist);
        }

        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            await base.DeleteRange<TEntity, TId>(entities);
        }

        public async Task DeleteRange(Expression<Func<TEntity, bool>> where)
        {
            await base.DeleteRange<TEntity, TId>(where);
        }

        public async Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false)
        {
            await base.SoftDelete<TEntity, TId>(id, exceptionRaiseIfNotExist);
        }
    }
}
