using Avesta.Language.Globalization.Enum;
using Avesta.Language.Globalization.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization.Provider
{
    public class LangFileProvider : LangContextProvider
    {

        #region [-Fields-]
        readonly string _dir;
        readonly string _suffix;
        #endregion


        #region [-Constructor-]
        public LangFileProvider(string dir = ".")
        {
            _dir = dir;
            _suffix = "lang";
        }
        #endregion




        #region [-Methods-]
        public async override Task Save()
        {
            await Task.CompletedTask;
        }

        public override async Task Write(GlobalWord globalWord)
        {
            var line = GetLineFormatOf(globalWord);
            foreach (var word in globalWord.Words ?? Enumerable.Empty<Word>())
            {
                await File.AppendAllTextAsync(GetFilePath(word.Language), line);
            }
        }
        public async override Task<string> ReadText(GlobalWord globalWord, LanguageShortName lang)
        {
            var result = await Read(globalWord.Key, lang);
            return result.ToString();
        }

        public async override Task<Word> Read(string key, LanguageShortName lang)
        {
            //open the file 
            //read each line 
            //try to parse each line
            //try to save each line in related data
            //try to transalte each line to related data
            //try to set content of translation of each data to related word
            //return word


            throw new NotImplementedException();
        }

        #endregion




        #region [-Dispose-]
        public async override ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
            GC.SuppressFinalize(this);
        }
        #endregion




        #region [-Internal Methods-]
        string GetFilePath(LanguageShortName lang)
        {
            var fileName = GetFileName(lang);
            var path = Path.Combine(_dir, fileName);
            return path;
        }

        string GetFileName(LanguageShortName lang) => Path.Combine(lang.ToString(), ".", _suffix);

        static string GetLineFormatOf(GlobalWord globalWord) => $"{globalWord.Key}=\t\t\t{(string.IsNullOrEmpty(globalWord.Comment) ? "#" + globalWord.Comment : string.Empty)}";

        #endregion





       
  

      
    }


}
