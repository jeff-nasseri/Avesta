
using Avesta.Data.Entity.Context;
using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Update
{
    public class UpdateRepository<TEntity, TId, TContext> : BaseUpdateRepository<TContext>, IUpdateRepository<TEntity, TId>
        where TId : class
        where TContext : AvestaDbContext
        where TEntity : BaseEntity<TId>
    {
        public UpdateRepository(TContext context) : base(context)
        {
        }

        public async Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false)
            => await base.Update<TEntity, TId>(entity, exceptionRaiseIfNotExist);

    }

}
