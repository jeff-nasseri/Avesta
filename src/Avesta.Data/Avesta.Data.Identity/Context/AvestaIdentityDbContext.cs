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
using Avesta.Data.Entity.Context;

namespace Avesta.Data.Identity.Context
{


    public abstract class AvestaIdentityDbContext<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup> : AvestaDbContext
        where TId : class
        where TAvestaUser : IAvestaUser<TId>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup, TAvestaUser>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId, TAvestaUser>
    {
        public AvestaIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public AvestaIdentityDbContext() : base()
        {
        }


        public DbSet<IAvestaUser<TId>> Users { get; set; }
        public DbSet<AvestaUserActivity<TId, IAvestaUser<TId>>>? UserActivities { get; set; }
        public DbSet<AvestaUserToken<TId, IAvestaUser<TId>>>? UserTokens { get; set; }
        public DbSet<AvestaUserClaim<TId, IAvestaUser<TId>>>? UserClaims { get; set; }
        public DbSet<TAvestaAuthorizeGroup>? AuthorizeGroups { get; set; }
        public DbSet<TUserAuthorizeGroup>? UserAuthorizeGroups { get; set; }





    }






}
