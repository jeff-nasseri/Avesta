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
            throw new NotImplementedException();
        }
        public async override Task<Word> Read(string id, LanguageShortName lang)
        {
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
            var path = Path.Combine(_dir, lang.ToString(), $".{_suffix}");
            return path;
        }

        static string GetLineFormatOf(GlobalWord globalWord) => $"{globalWord.Key}=\t\t\t{(string.IsNullOrEmpty(globalWord.Comment) ? "#" + globalWord.Comment : string.Empty)}";

        #endregion





       
  

      
    }


}
