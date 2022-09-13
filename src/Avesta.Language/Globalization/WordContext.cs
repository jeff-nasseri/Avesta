using Avesta.Language.Globalization.Provider;
using System.Data;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization
{
    public abstract class WordContext
    {
        readonly LangContextProvider _provider;
        public WordContext(LangContextProvider provider)
        {
            _provider = provider;
        }



        /// <summary>
        /// Create a file or any context for writing language data on it
        /// </summary>
        public async virtual Task OnCreate()
        {
            //get all global word of TWordContext by reflection
        }


        /// <summary>
        /// Fill all GlobalWord.Words type fields in application WordContext
        /// </summary>
        public async virtual Task OnInitialize()
        {
            //fill content of all word in Twordcontext global words
        }



    }

}
