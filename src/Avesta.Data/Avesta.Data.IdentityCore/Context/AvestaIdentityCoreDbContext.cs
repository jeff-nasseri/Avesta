using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avesta.Data.Entity.Model;
using Avesta.Data.IdentityCore.Model;
using Avesta.Data.Identity.Model;

namespace Avesta.Data.Context
{
    public class AvestaIdentityCoreDbContext<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup> : AvestaIdentityDbContext<TId, TAvestaUser>
        where TId : class, IEquatable<TId>
        where TAvestaUser : AvestaIdentityUser<TId>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup, TAvestaUser>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId, TAvestaUser>
    {
        public AvestaIdentityCoreDbContext(DbContextOptions options)
          : base(options)
        {

        }


        public DbSet<TAvestaAuthorizeGroup>? AuthorizeGroups { get; set; }
        public DbSet<TUserAuthorizeGroup>? UserAuthorizeGroups { get; set; }




    }





    public class AvestaIdentityDbContext<TId, TAvestaUser> : IdentityDbContext<TAvestaUser, IdentityRole<TId>, TId>
        where TId : class, IEquatable<TId>
        where TAvestaUser : AvestaIdentityUser<TId>
    {
        public AvestaIdentityDbContext(DbContextOptions options)
          : base(options)
        {

        }



    }
}
