using Avesta.Language.Globalization.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Language.Globalization.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWordContext<TWordContext, TLangProvider>(this IServiceCollection service)
        where TWordContext : WordContext
        where TLangProvider : LangContextProvider
        {
            service.AddSingleton<TLangProvider>();
            service.AddSingleton<TWordContext>();

            //TWordContext.OnConfiguration()
            //TWordContext.OnInitialize()

            return service;
        }
    }
}
