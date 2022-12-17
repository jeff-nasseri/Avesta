using Avesta.Language.Globalization.Provider;
using Microsoft.Extensions.DependencyInjection;
using Avesta.Language.Globalization.Enum;
using Avesta.Language.Globalization.Extension;
using Avesta.Language.Globalization.Model;

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

            return service;
        }
    }
}
