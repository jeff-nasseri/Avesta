using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepositoryRepository
{

    public class EntityRepository<TContext> : BaseRepository<TContext, string>
        where TContext : DbContext
    {
        public EntityRepository(TContext context) : base(context)
        {
        }
    }



}
