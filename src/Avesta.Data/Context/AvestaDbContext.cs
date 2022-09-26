using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Avesta.Data.Context
{
    public class AvestaDbContext<TAvestaUser> : IdentityDbContext<TAvestaUser> where TAvestaUser : IdentityUser
    {
        public AvestaDbContext(DbContextOptions options)
        : base(options)
        {

        }

        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use reflection to write this code
            //modelBuilder.Entity<Like>().HasQueryFilter(u => !u.DeletedDate.HasValue);
            base.OnModelCreating(modelBuilder);

        }

    }

}
