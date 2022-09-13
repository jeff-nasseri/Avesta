using Avesta.Language.Globalization.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Language.Globalization.Extension
{
    public static class DependencyInjection
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
