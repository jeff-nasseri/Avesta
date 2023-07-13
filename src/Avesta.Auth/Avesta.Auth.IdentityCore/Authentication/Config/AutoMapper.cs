using Avesta.Auth.IdentityCore.Authentication.ViewModel;
using Avesta.Data.IdentityCore.Model;
using Avesta.Data.Entity.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Auth.IdentityCore.Authentication.Config
{
    public static class AutoMapper
    {
        public static IServiceCollection ConfigureMapperForAuthentication<TId, TAvestaUser>(this IServiceCollection services)
            where TId : class, IEquatable<TId>
            where TAvestaUser : AvestaIdentityUser<TId>
        {
            services.AddAutoMapper(config =>
            {
                #region register view model

                config.CreateMap<RegisterUserViewModel<TId>, TAvestaUser>()
                .ReverseMap();

                #endregion



            });



            return services;
        }
    }
}
