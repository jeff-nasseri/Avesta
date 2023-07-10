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
using Avesta.Data.Identity.Model;

namespace Avesta.Data.Identity.Context
{


    public class AvestaIdentityDbContext<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup> : DbContext
        where TId : class
        where TAvestaUser : AvestaUser<TId>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
        public AvestaIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public AvestaIdentityDbContext() : base()
        {
        }

        public DbSet<TAvestaUser> User { get; set; }
        public DbSet<TAvestaAuthorizeGroup>? AuthorizeGroups { get; set; }
        public DbSet<TUserAuthorizeGroup>? UserAuthorizeGroups { get; set; }
        public DbSet<AvestaUserActivity<TId, TAvestaUser>>? UserActivities { get; set; }
        public DbSet<AvestaUserToken<TId, TAvestaUser>>? UserTokens { get; set; }


    }



}
