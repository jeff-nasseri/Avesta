using Avesta.Data.Model;
using Avesta.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository
{



    public class EntityRepository<TEntity, TContext> : EntityRepository<TEntity, TContext, string>
    where TEntity : BaseEntity<string>
    where TContext : DbContext
    {
        public EntityRepository(TContext context) : base(context)
        {
        }
    }
}
