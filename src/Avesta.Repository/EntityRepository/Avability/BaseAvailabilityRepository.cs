using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Exceptions.Entity;
using Avesta.Repository.EntityRepositoryRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Avability
{
    public class BaseAvailabilityRepository<TContext> : BaseRepo<TContext>
        where TContext : AvestaDbContext, new()
    {
        readonly TContext _context;
        public BaseAvailabilityRepository(TContext context) : base(context)
        {
            _context = context;
        }


        public async Task<bool> Any<TEntity, TId>(Expression<Func<TEntity, bool>> any, string navigationPropertyPath = null)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var result = false;

            if (string.IsNullOrEmpty(navigationPropertyPath))
                result = await base.Table<TEntity, TId>().AnyAsync(any);
            else
                result = await base.IncludeByPath<TEntity, TId>(navigationPropertyPath).AnyAsync(any);

            return result;
        }


        public async Task CheckAvailability<TEntity, TId>(Expression<Func<TEntity, bool>> any, string navigationPropertyPath = null)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var result = false;

            if (string.IsNullOrEmpty(navigationPropertyPath))
                result = await base.Table<TEntity, TId>().AnyAsync(any);
            else
                result = await base.IncludeByPath<TEntity, TId>(navigationPropertyPath).AnyAsync(any);


            if (!result)
                throw new CanNotFoundEntityException(any.ToString());
        }


    }

}
