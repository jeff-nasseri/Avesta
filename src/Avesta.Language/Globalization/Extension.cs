using Avesta.Globalization.Language;
using Microsoft.Extensions.DependencyInjection;

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

            return service;
        }
    }


}

