using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avesta.Data.Model;

namespace Avesta.Data.Context
{
    public class AvestaIdentityDbContext<TAvestaUser> : IdentityDbContext<TAvestaUser> where TAvestaUser : AvestaUser
    {
        public AvestaIdentityDbContext(DbContextOptions options)
          : base(options)
        {

        }


        public DbSet<AvestaAuthorizeGroup> AuthorizeGroups { get; set; }
        public DbSet<UserAuthorizeGroup> UserAuthorizeGroups { get; set; }


        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use reflection to write this code
            //modelBuilder.Entity<Like>().HasQueryFilter(u => !u.DeletedDate.HasValue);
            base.OnModelCreating(modelBuilder);

        }

    }
}
