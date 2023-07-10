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
    public class AvestaIdentityCoreDbContext<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup> : AvestaIdentityDbContext<TAvestaUser>
        where TId : class
        where TAvestaUser : AvestaIdentityUser<TId, TUserAuthorizeGroup>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
        public AvestaIdentityCoreDbContext(DbContextOptions options)
          : base(options)
        {

        }


        public DbSet<TAvestaAuthorizeGroup>? AuthorizeGroups { get; set; }
        public DbSet<TUserAuthorizeGroup>? UserAuthorizeGroups { get; set; }




    }





    public class AvestaIdentityDbContext<TAvestaUser> : IdentityDbContext<TAvestaUser>
    where TAvestaUser : AvestaIdentityUser
    {
        public AvestaIdentityDbContext(DbContextOptions options)
          : base(options)
        {

        }



    }
}
