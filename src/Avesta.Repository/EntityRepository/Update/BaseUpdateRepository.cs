using Avesta.Data.Context;
using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Update
{
    public class BaseUpdateRepository<TEntity, TId, TContext>
        where TId : class
        where TContext : AvestaDbContext
        where TEntity : BaseEntity<TId>
    {
        readonly TContext _context;
        public BaseUpdateRepository(TContext context)
        {
            _context = context;
        }

    }

}
