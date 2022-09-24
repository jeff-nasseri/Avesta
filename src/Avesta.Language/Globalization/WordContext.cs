using Avesta.Language.Globalization.Model;
using Avesta.Language.Globalization.Provider;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
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
            var globalWords = this.GetType().GetFields().ToList().Select(c=>c.GetValue(this) as GlobalWord).ToList();
            foreach (var globalWord in globalWords)
            {
                await _provider.Write(globalWord);
            }
            await OnSaveChange();
        }


        /// <summary>
        /// Fill all GlobalWord.Words type fields in application WordContext
        /// </summary>
        public async virtual Task OnInitialize()
        {
            //fill content of all word in Twordcontext global words
        }


        /// <summary>
        /// Save context
        /// </summary>
        public async virtual Task OnSaveChange()
        {
            await _provider.Save();
        }


    }


}
