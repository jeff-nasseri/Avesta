using Avesta.Globalization.Language;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization
{
    public class LangFileProvider : LangContextProvider
    {
        readonly string _dir;
        readonly string _suffix;

        public LangFileProvider(string dir = ".")
        {
            _dir = dir;
            _suffix = "json";
        }

        public async override ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
            GC.SuppressFinalize(this);
        }

        public async override Task Save()
        {
            await Task.CompletedTask;
        }

        public override async Task Write(Word word)
        {
            var data = GetJson(word);
            await File.WriteAllTextAsync(GetFilePath(word.Language), data);
        }

        string GetFilePath(LanguageShortName lang)
        {
            var path = Path.Combine(_dir, lang.ToString(), $".{_suffix}");
            return path;
        }

        static string GetJson(Word word)
        {
            var result = JsonConvert.SerializeObject(word);
            return result;
        }

        public async override Task WriteRange(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                await Write(word);
            }
        }

        public async override Task<Word> Read(string id, LanguageShortName lang)
        {
            await Task.CompletedTask;
            var filePath = GetFilePath(lang);
            var txt = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<IEnumerable<Word>>(txt);
            var result = data.SingleOrDefault(w => w.Guid == id);
            return result;
        }
    }


}
