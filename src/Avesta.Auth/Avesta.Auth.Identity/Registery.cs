using Avesta.Data.Identity.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Identity
{
    public static class RegisteryExtension
    {
        public static IServiceCollection RegisterIdentity<TId
                , TAvestaUser
                , TAvestaUserAuthorizeGroup
                , TAuthorizeGroup
                , TAvestaUserToken
                , TAvestaUserActivity
                , TUserHandler>(this IServiceCollection services)
            where TId : class
            where TAvestaUser : AvestaUser<TId>
            where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId, TAvestaUser>
            where TAuthorizeGroup : AvestaAuthorizeGroup<TId, TAvestaUserAuthorizeGroup, TAvestaUser>
            where TAvestaUserToken : AvestaUserToken<TId, TAvestaUser>
            where TAvestaUserActivity : AvestaUserActivity<TId, TAvestaUser>
            where TUserHandler : AvestaUserHandler<TId, TAvestaUser, TAuthorizeGroup, TAvestaUserAuthorizeGroup, TAvestaUserToken, TAvestaUserActivity>
        {

            services.AddScoped<TUserHandler>();
            return services;
        }

        public static IServiceCollection RegisterIdentity<TId, TAvestaUser, TUserManager>(this IServiceCollection services)
            where TId : class
            where TAvestaUser : AvestaUser<TId>
            where TUserManager : AvestaUserManager<TId, TAvestaUser>
        {

            services.AddScoped<TUserManager>();
            return services;
        }

        public static IServiceCollection RegisterIdentity<TAvestaUser, TUserManager>(this IServiceCollection services)
            where TAvestaUser : AvestaUser<string>
            where TUserManager : AvestaUserManager<TAvestaUser>
        {

            services.AddScoped<TUserManager>();
            return services;
        }

        public static IServiceCollection RegisterIdentity<TAvestaUser>(this IServiceCollection services)
            where TAvestaUser : AvestaUser<string>
        {

            services.AddScoped<AvestaUserManager<TAvestaUser>>();
            return services;
        }
    }

}
