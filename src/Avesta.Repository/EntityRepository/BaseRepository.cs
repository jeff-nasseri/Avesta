using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Entity
{
    public abstract class BaseRepository<TContext>
        where TContext : DbContext
    {
        readonly TContext _context;
        public BaseRepository(TContext context)
        {
            _context = context;
        }
        public async Task Delete(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
