using Avesta.Data.Context;
using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Update
{
    internal class UpdateRepository
    {
    }
    public class UpdateRepository<TEntity, TId, TContext> : BaseUpdateRepository<TEntity, TId, TContext>, IUpdateRepository<TEntity, TId>
        where TId : class
        where TContext : AvestaDbContext
        where TEntity : BaseEntity<TId>
    {
        public UpdateRepository(TContext context) : base(context)
        {
        }

        public Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false)
        {
            throw new NotImplementedException();
        }
    }

}
