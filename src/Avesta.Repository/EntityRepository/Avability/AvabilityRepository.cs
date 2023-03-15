using Avesta.Data.Context;
using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Avability
{
    public class AvailabilityRepository<TEntity, TId, TContext> : BaseAvailabilityRepository<TContext>, IAvailabilityRepository<TEntity, TId>
        where TId : class
        where TContext : AvestaDbContext, new()
        where TEntity : BaseEntity<TId>
    {
        public AvailabilityRepository(TContext context) : base(context)
        {
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath = null)
        {
            var result = await base.Any<TEntity, TId>(any, navigationPropertyPath);
            return result;
        }

        public async Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath = null)
        {
            await base.CheckAvailability<TEntity, TId>(any, navigationPropertyPath);
        }
    }

}
