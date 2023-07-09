
using Avesta.Data.Entity.Context;
using Avesta.Data.Entity.Model;
using Avesta.Exceptions.Entity;
using Avesta.Repository.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Availability
{
    public class BaseAvailabilityRepository<TContext> : BaseRepository<TContext>
        where TContext : AvestaDbContext
    {
        readonly TContext _context;
        public BaseAvailabilityRepository(TContext context) : base(context)
        {
            _context = context;
        }


        public async Task<bool> Any<TEntity, TId>(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
         where TId : class
         where TEntity : BaseEntity<TId>
            => await Any<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), any);



        public async Task<bool> Any<TEntity, TId>(Expression<Func<TEntity, bool>> any)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await Any<TEntity, TId>(base.Query<TEntity, TId>(), any);




        public async Task<bool> Any<TEntity, TId>(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var result = await entities.AnyAsync(any);
            return result;
        }




        public async Task CheckAvailability<TEntity, TId>(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
           where TId : class
           where TEntity : BaseEntity<TId>
                => await CheckAvailability<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), any);


        public async Task CheckAvailability<TEntity, TId>(Expression<Func<TEntity, bool>> any)
           where TId : class
           where TEntity : BaseEntity<TId>
                => await CheckAvailability<TEntity, TId>(base.Query<TEntity, TId>(), any);


        public async Task CheckAvailability<TEntity, TId>(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
           where TId : class
           where TEntity : BaseEntity<TId>
        {
            var result = await entities.AnyAsync(any);

            if (!result)
                throw new CanNotFoundEntityException(any.ToString());
        }


    }

}
