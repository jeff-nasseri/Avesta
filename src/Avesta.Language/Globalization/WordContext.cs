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

        public abstract Task<string> GetMessageInCurrentActiveLanguage(object key);



        /// <summary>
        /// Create a file or any context for writing language data on it
        /// </summary>
        public async virtual Task OnCreate()
        {
            var globalWords = this.GetType().GetFields().ToList().Select(c => c.GetValue(this) as GlobalWord).ToList();
            foreach (var globalWord in globalWords)
            {
                await _provider.Write(globalWord);
            }
            await OnSaveChange();
        }


        /// <summary>
        /// Fill content of all GlobalWord.Words type fields in application WordContext
        /// </summary>
        public async virtual Task OnInitialize()
        {
            var globalWords = this.GetType().GetFields().ToList().Select(c => c.GetValue(this) as GlobalWord).ToList();
            foreach (var globalWord in globalWords)
            {
                var key = globalWord.Key;
                foreach (var word in globalWord.Words)
                {
                    var content = await _provider.Read(key, word.Language);
                    word.OnSetContent(content);
                }
            }
        }



        /// <summary>
        /// Save context
        /// </summary>
        public async virtual Task OnSaveChange()
        {
            await _provider.Save();
        }



        /// <summary>
        /// This method call OnCreate and then OnInitialize method
        /// </summary>
        public async virtual Task ToWayInitialize()
        {
            await OnCreate();
            await OnInitialize();
        }




    }


}
