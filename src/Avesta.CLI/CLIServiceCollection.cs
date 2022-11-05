using Avesta.Attribute.CLI;
using Avesta.CLI.Context;
using Avesta.CLI.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.CLI
{
    public static class CLIServiceCollection
    {
        public static IServiceCollection ProcessAvestaCLI(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<ClassAttribute, CLIClassModel>().ReverseMap();
                config.CreateMap<ArgumentAttribute, CLIArgumentModel>().ReverseMap();
                config.CreateMap<MethodAttribute, CLIMethodModel>().ReverseMap();
                config.CreateMap<PropertyAttribute, CLIPropertyModel>().ReverseMap();
                config.CreateMap<PropertyAttribute, CLIPropertyModel>().ReverseMap();
            })
            .AddSingleton<AvestaCommandContext>()
            .AddSingleton<Core>()
            .AddSingleton<CommandExecuter>();




            return services;
        }

        public static IServiceCollection ProcessAvestaCLI(this IServiceCollection services, params Type[] cliTypes)
        {
            services.ProcessAvestaCLI();

            foreach (var type in cliTypes)
            {
                services.AddScoped(type);
            }

            return services;
        }
    }
}
