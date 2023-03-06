using Avesta.Data.Context;
using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Test
{
    public class ServiceResolver<TEntity, TContext> : RepositoryResolver<TEntity, TContext>
        where TEntity : BaseEntity
        where TContext : AvestaDbContext
    {

    }
}
