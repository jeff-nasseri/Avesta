using Avesta.Language.Globalization.Provider;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization
{
    public abstract class WordContext
    {
        public async virtual Task OnCreate(LangContextProvider provider)
        {
        }
    }

}
