using AutoMapper;
using Avesta.Data.Model;
using Avesta.Share.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Mapper
{
    public static class ConfigureAutoMapper
    {
        public static IServiceCollection ConfigureMapper<TModel, TViewModel, TEditViewModel, TCreateViewModel>(this IServiceCollection services)
            where TModel : BaseEntity
            where TViewModel : BaseModel
            where TEditViewModel : BaseModel
            where TCreateViewModel : BaseModel
        {
            services.AddAutoMapper(config =>
            {
                #region register view model

                config.CreateMap<TModel, TViewModel>()
                .ReverseMap();

                config.CreateMap<TModel, TViewModel>()
                .ForMember(src => src.CreatedTime, opt => opt.MapFrom(dest => dest.CreatedDate));

                config.CreateMap<TViewModel, TEditViewModel>()
                .ReverseMap();

                config.CreateMap<TViewModel, TCreateViewModel>()
                .ReverseMap();

                #endregion
            });



            return services;
        }



        public static IServiceCollection ConfigureMapper<TModel, TViewModel>(this IServiceCollection services)
            where TModel : class
            where TViewModel : class
        {
            services.AddAutoMapper(config =>
            {
                #region register view model

                config.CreateMap<TModel, TViewModel>()
                .ReverseMap();

                #endregion
            });



            return services;
        }



        public static IServiceCollection ConfigureMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> configure)
        {
            services.AddAutoMapper(configure);

            return services;
        }



    }
}
