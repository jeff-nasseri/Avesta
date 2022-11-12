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
        readonly string _dir = ".\\language";
        readonly string _suffix = "lang";
        #endregion


        #region [-Properties-]
        protected Stream Stream { get; private set; }
        protected StreamWriter Writer { get; private set; }
        protected StreamReader Reader { get; private set; }
        #endregion


        #region [-Constructor-]
        public LangFileProvider(string dir, string suffix) : this()
        {
            _dir = dir;
            _suffix = suffix;
        }
        public LangFileProvider()
        {
            if(!Directory.Exists(_dir))
                Directory.CreateDirectory(_dir);
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
                var exist = await IsKeyExist(globalWord.Key, word.Language);
                if (exist)
                    continue;

                var path = GetFilePath(word.Language);
                await File.AppendAllTextAsync(path, line);
            }
        }
        public async override Task<string> ReadText(GlobalWord globalWord, LanguageShortName lang)
        {
            var result = await Read(globalWord.Key, lang);
            return result.ToString();
        }

        public async override Task<string> Read(object key, LanguageShortName lang)
        {
            var path = GetFilePath(lang);
            var line = (await File.ReadAllLinesAsync(path)).SingleOrDefault(l => l.Trim().ToLower().StartsWith(key.ToString().Trim().ToLower()));
            var parts = line.Split('=');
            var message = parts[1].Substring(0, parts[1].IndexOf("#"));
            return message.Trim();
        }

        public async override Task<bool> IsKeyExist(object key, LanguageShortName language)
        {
            await Task.CompletedTask;
            var path = GetFilePath(language);
            if (!File.Exists(path))
                return false;

            var result = File.ReadAllLines(path).Any(l => l.Trim().ToLower().StartsWith(key.ToString().Trim().ToLower()));
            return result;
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
        public virtual string GetFilePath(LanguageShortName lang)
        {
            var fileName = GetFileName(lang);
            var path = Path.Combine(_dir, fileName);
            return path;
        }

        public virtual string GetFileName(LanguageShortName lang) => $"{lang}.{_suffix}";

        public virtual string GetLineFormatOf(GlobalWord globalWord) => $"{globalWord.Key}=\t\t\t{(!string.IsNullOrEmpty(globalWord.Comment) ? "#" + globalWord.Comment : string.Empty)}\n";



        #endregion









    }


}
