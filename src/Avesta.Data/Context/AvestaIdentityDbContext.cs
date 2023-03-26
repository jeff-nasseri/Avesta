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
    public class AvestaIdentityDbContext<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup> : AvestaIdentityDbContext<TAvestaUser>
        where TId : class
        where TAvestaUser : AvestaUser<TId, TUserAuthorizeGroup>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
        public AvestaIdentityDbContext(DbContextOptions options)
          : base(options)
        {

        }


        public DbSet<TAvestaAuthorizeGroup>? AuthorizeGroups { get; set; }
        public DbSet<TUserAuthorizeGroup>? UserAuthorizeGroups { get; set; }




    }





    public class AvestaIdentityDbContext<TAvestaUser> : IdentityDbContext<TAvestaUser>
    where TAvestaUser : AvestaUser
    {
        public AvestaIdentityDbContext(DbContextOptions options)
          : base(options)
        {

        }



    }
}
