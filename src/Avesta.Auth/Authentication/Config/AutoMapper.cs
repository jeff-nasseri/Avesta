using Avesta.Auth.Authentication.ViewModel;
using Avesta.Data.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Auth.Authentication.Config
{
    public static class AutoMapper
    {
        public static IServiceCollection ConfigureMapperForAuthentication<TAvestaUser>(this IServiceCollection services)
            where TAvestaUser : AvestaUser
        {
            services.AddAutoMapper(config =>
            {
                #region register view model

                config.CreateMap<RegisterUserViewModel, TAvestaUser>()
                .ReverseMap();
                
                #endregion



            });



            return services;
        }
    }
}
