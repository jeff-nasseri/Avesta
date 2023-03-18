﻿using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Exceptions.Entity;
using Avesta.Repository.EntityRepositoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Update
{
    public class BaseUpdateRepository<TContext> : BaseRepository<TContext>
        where TContext : AvestaDbContext
    {
        public BaseUpdateRepository(TContext context) : base(context)
        {
        }


        public async Task Update<TEntity,TId>(TEntity entity, bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var data = base.Table<TEntity,TId>().Find(entity);

            if (exceptionRaiseIfNotExist && data == null)
                throw new CanNotFoundEntityException(entity.ID);


            if(entity != null)
            {
                base.Table<TEntity, TId>().Update(entity);
                await base.SaveChanges();
            }


        }

    }

}
