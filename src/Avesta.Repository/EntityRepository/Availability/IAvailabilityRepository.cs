using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Availability
{
    public interface IAvailabilityRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {


        Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath);

        Task<bool> Any(Expression<Func<TEntity, bool>> any);

        Task<bool> Any(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any);





        Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath);

        Task CheckAvailability(Expression<Func<TEntity, bool>> any);

        Task CheckAvailability(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any);
    }

}
