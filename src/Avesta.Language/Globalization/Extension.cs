using Avesta.Globalization.Language;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization
{

    public static class ServiceExtension
    {
        public static IServiceCollection AddWordContext<TContext, TLangProvider>(this IServiceCollection service)
            where TContext : WordContext
            where TLangProvider : LangContextProvider
        {
            service.AddSingleton<TLangProvider>();
            service.AddSingleton<TContext>();

            WordContext context;
            LangContextProvider provider;

            //await context.OnCreate(provider);

            return service;
        }
    }

}
