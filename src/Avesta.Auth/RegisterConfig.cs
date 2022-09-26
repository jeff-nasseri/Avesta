using Avesta.Auth.Authentication.Service;
using Avesta.Auth.HTTP.Service;
using Avesta.Auth.JWT.Model;
using Avesta.Auth.JWT.Service;
using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Auth
{

    public static class RegisterConfig
    {
        public static IServiceCollection AddAvestaAuthentication<TAvestaUser, TRole, TAvestaDbContext, TIdentityErrorDescriptor>
            (this IServiceCollection service
            , Action<AvestaAuthenticationOption> setupActionForAvestaAuth
            , Action<IdentityOptions> setupActionForIdentity
            , Action<AuthenticationOptions> configureAuthentication
            , Action<JwtBearerOptions> configureJwtOptions)
            where TAvestaUser : AvestaUser
            where TRole : IdentityRole
            where TAvestaDbContext : AvestaDbContext<TAvestaUser>
            where TIdentityErrorDescriptor : IdentityErrorDescriber
        {

            #region [-Register JWTAuth-]
            service.AddSingleton<IJWTAuthenticationService, JWTAuthenticationService<TAvestaUser, TRole>>();
            service.AddScoped<IIdentityRepository<TAvestaUser, TRole>, IdentityRepository<TAvestaUser, TRole>>();
            var option = new AvestaAuthenticationOption();
            setupActionForAvestaAuth(option);
            service.AddSingleton(option);
            #endregion


            #region [-Register Identity Setting & Services-]
            service.SetIdentityWithIdentityCore<TAvestaUser, TRole, TAvestaDbContext, TIdentityErrorDescriptor>
                            (setupActionForIdentity, configureAuthentication, configureJwtOptions);
            #endregion


            #region [-Register HttpAuth-]
            service.AddSingleton<IHttpAuthService<TAvestaUser>, HttpAuthService<TAvestaUser, TRole>>();
            #endregion


            #region [-Register Authentication-]
            service.AddSingleton<IAuthenticationService<TAvestaUser>, AuthenticationService<TAvestaUser, TRole>>();
            #endregion


            return service;
        }



        static IServiceCollection SetIdentityWithIdentityCore<TAvestaUser, TRole, TAvestaDbContext, TIdentityErrorDescriptor>
            (this IServiceCollection services
            , Action<IdentityOptions> setupActionForIdentity
            , Action<AuthenticationOptions> configureAuthentication
            , Action<JwtBearerOptions> configureJwtOptions)
            where TAvestaUser : AvestaUser
            where TRole : IdentityRole
            where TAvestaDbContext : AvestaDbContext<TAvestaUser>
            where TIdentityErrorDescriptor : IdentityErrorDescriber
        {
            services.AddIdentity<TAvestaUser, TRole>(setupActionForIdentity)
            .AddEntityFrameworkStores<TAvestaDbContext>()
            .AddDefaultTokenProviders().AddErrorDescriber<TIdentityErrorDescriptor>();

            services.AddAuthentication(configureAuthentication)
           .AddJwtBearer(configureJwtOptions);


            return services;

        }



    }


}
