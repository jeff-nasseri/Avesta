using Avesta.Auth.Authentication.Config;
using Avesta.Auth.Authentication.Service;
using Avesta.Auth.Authorize.Service;
using Avesta.Auth.User.Service;
using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.HTTP.Auth.Service;
using Avesta.HTTP.JWT.Model;
using Avesta.HTTP.JWT.Service;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.EntityRepositoryRepository;
using Avesta.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Auth
{

    public static class RegisterConfig
    {
        public static IServiceCollection AddAvestaAuthentication<TAvestaUser, TRole>(
            this IServiceCollection service
            , Action<AvestaAuthenticationOption> setupActionForAvestaAuth)
            where TAvestaUser : AvestaUser
            where TRole : IdentityRole
        {

            #region [-Register JWTAuth-]
            service.AddScoped<IJWTAuthenticationService, JWTAuthenticationService<TAvestaUser, TRole>>();
            service.AddScoped<IIdentityRepository<TAvestaUser, TRole>, IdentityRepository<TAvestaUser, TRole>>();
            var option = new AvestaAuthenticationOption();
            setupActionForAvestaAuth(option);
            service.AddSingleton(option);
            #endregion


            #region [-Register HttpAuth-]
            service.AddScoped<IHttpAuthService<TAvestaUser>, HttpAuthService<TAvestaUser, TRole>>();
            #endregion


            #region [-Register Authentication-]
            service.AddScoped<IAuthenticationService<TAvestaUser>, AuthenticationService<TAvestaUser, TRole>>();
            #endregion


            #region [-Configure AutoMapper-]
            service.ConfigureMapperForAuthentication<TAvestaUser>();
            #endregion



            #region [-Register User-]
            service.AddScoped<IUserService<TAvestaUser>, UserService<TAvestaUser, TRole>>();
            #endregion

            return service;
        }





        public static IServiceCollection AddAvestaAuthorization<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup, TAvestaDbContext>(this IServiceCollection service)

            where TAvestaUser : AvestaUser<TAvestaUserAuthorizeGroup>
            where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TAvestaUserAuthorizeGroup>
            where TAvestaDbContext : AvestaIdentityDbContext<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
            where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup
        {

            service.AddScoped<IRepository<TAvestaAuthorizeGroup>, EntityRepository<TAvestaAuthorizeGroup, TAvestaDbContext>>();
            service.AddScoped<IRepository<TAvestaUserAuthorizeGroup>, EntityRepository<TAvestaUserAuthorizeGroup, TAvestaDbContext>>();


            service.AddScoped<IAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
                , AuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>>();

            service.AddScoped<IAvestaUserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
                , AvestaUserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>>();

            return service;
        }




    }


}
