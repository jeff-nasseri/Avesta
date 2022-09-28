using Avesta.Data.Model;
using Avesta.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                config.CreateMap<TViewModel, TEditViewModel>()
                .ReverseMap();

                config.CreateMap<TViewModel, TCreateViewModel>()
                .ReverseMap();

                #endregion
            });



            return services;
        }



        public static IServiceCollection ConfigureMapper<TModel, TViewModel>(this IServiceCollection services)
            where TModel : BaseEntity
            where TViewModel : BaseModel
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



    }
}
