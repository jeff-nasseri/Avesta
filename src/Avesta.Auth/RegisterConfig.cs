using Avesta.Auth.Authentication.Config;
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


            return service;
        }







    }


}
