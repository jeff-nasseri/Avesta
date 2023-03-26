using Avesta.Auth.Authentication.Config;
using Avesta.Auth.Authentication.Service;
using Avesta.Auth.Authorize.Service;
using Avesta.Auth.User.Service;
using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.HTTP.Auth.Service;
using Avesta.HTTP.JWT.Model;
using Avesta.HTTP.JWT.Service;
using Avesta.Repository;
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





        public static IServiceCollection AddAvestaAuthorization<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup, TAvestaDbContext>(this IServiceCollection service)
            where TId : class
            where TAvestaUser : AvestaUser<TId, TAvestaUserAuthorizeGroup>
            where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TAvestaUserAuthorizeGroup>
            where TAvestaDbContext : AvestaDbContext
            where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
        {

            service.RegisterRepositories<TId, TAvestaAuthorizeGroup, TAvestaDbContext>();
            service.RegisterRepositories<TId, TAvestaUserAuthorizeGroup, TAvestaDbContext>();


            service.AddScoped<IAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
                , AuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>>();

            service.AddScoped<IAvestaUserAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
                , AvestaUserAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>>();

            return service;
        }




    }


}
