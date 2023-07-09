
using Avesta.Data.Entity.Context;
using Avesta.Data.Entity.Model;
using Avesta.Exceptions.Entity;
using Avesta.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Delete
{
    public class BaseDeleteRepository<TContext> : BaseRepository<TContext>
        where TContext : AvestaDbContext
    {
        public BaseDeleteRepository(TContext context) : base(context)
        {
        }

        public async Task Delete<TEntity, TId>(TEntity entity, bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {

            var data = await base.Table<TEntity, TId>().FindAsync(entity);

            if (exceptionRaiseIfNotExist && data == null)
                throw new CanNotFoundEntityException(entity.ID);

            base.Table<TEntity, TId>().Remove(entity);
            await base.SaveChanges();
        }

        public async Task Delete<TEntity, TId>(TId id, bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var data = await base.Table<TEntity, TId>().FindAsync(id);

            if (exceptionRaiseIfNotExist && data == null)
                throw new CanNotFoundEntityException(id);

            base.Table<TEntity, TId>().Remove(data);
            await base.SaveChanges();
        }

        public async Task DeleteRange<TEntity, TId>(IEnumerable<TEntity> entities)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            base.Table<TEntity, TId>().RemoveRange(entities);
            await base.SaveChanges();
        }


        public async Task Delete<TEntity,TId>(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var data = base.Table<TEntity, TId>().SingleOrDefault(single);

            if (exceptionRaiseIfNotExist && data == null)
                throw new CanNotFoundEntityException(single.ToString());

            base.Table<TEntity, TId>().Remove(data);
            await base.SaveChanges();

        }


        public async Task DeleteRange<TEntity,TId>(Expression<Func<TEntity, bool>> where)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var data = base.Table<TEntity, TId>().Where(where);

            base.Table<TEntity, TId>().RemoveRange(data);

            await base.SaveChanges();
        }



        public async Task SoftDelete<TEntity, TId>(TId id, bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var entity = await base.Table<TEntity, TId>().FindAsync(id);

            if (exceptionRaiseIfNotExist && entity == null)
                throw new CanNotFoundEntityException(id);

            entity.SoftDelete();
            entity.ModifiedDate = DateTime.UtcNow;

            await base.SaveChanges();
        }


    }

}
