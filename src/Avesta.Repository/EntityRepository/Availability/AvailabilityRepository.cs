using Avesta.Data.Context;
using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Availability
{
    public class AvailabilityRepository<TEntity, TId, TContext> : BaseAvailabilityRepository<TContext>, IAvailabilityRepository<TEntity, TId>
        where TId : class
        where TContext : AvestaDbContext, new()
        where TEntity : BaseEntity<TId>
    {
        public AvailabilityRepository(TContext context) : base(context)
        {
        }





        public async Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
             => await base.Any<TEntity, TId>(any, navigationPropertyPath);

        public async Task<bool> Any(Expression<Func<TEntity, bool>> any)
             => await base.Any<TEntity, TId>(any);

        public async Task<bool> Any(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
             => await base.Any<TEntity, TId>(entities, any);





        public async Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
             => await base.CheckAvailability<TEntity, TId>(any, navigationPropertyPath);

        public async Task CheckAvailability(Expression<Func<TEntity, bool>> any)
             => await base.CheckAvailability<TEntity, TId>(any);

        public async Task CheckAvailability(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
             => await base.CheckAvailability<TEntity, TId>(entities, any);
    }

}
