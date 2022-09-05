using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Avesta.Globalization.Language
{

    public abstract class LangContextProvider : IAsyncDisposable
    {
        public abstract Task Write(Word word);
        public abstract Task WriteRange(IEnumerable<Word> words);
        public abstract Task Save();
        public abstract Task<Word> Read(string id, Language lang);
        public abstract ValueTask DisposeAsync();

    }

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

        string GetFilePath(Language lang)
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

        public async override Task<Word> Read(string id, Language lang)
        {
            await Task.CompletedTask;
            var filePath = GetFilePath(lang);
            var txt = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<IEnumerable<Word>>(txt);
            var result = data.SingleOrDefault(w => w.Guid == id);
            return result;
        }
    }


    public abstract class WordContext
    {

        public async virtual Task OnCreate(LangContextProvider provider)
        {

        }
    }



    public class Globalization
    {
    }

    public class TestWordContext : WordContext
    {
        readonly LangContextProvider _provider;
        public TestWordContext(LangContextProvider provider)
        {
            _provider = provider;
            OnCreate(provider).Wait();
        }




        public static WordList Username = new WordList(new Word("username", Language.US_EN, ContentType.Display));






    }
    public class WordList
    {
        public IEnumerable<Word> Words { get; set; } = new List<Word>();

        public WordList(params Word[] data)
        {
            foreach (var item in data)
            {
                Words.Append(item);
            }
        }
    }

    public class Word : Globalization, IComparable<Word>
    {
        public Word()
        {
            Guid = System.Guid.NewGuid().ToString();
        }
        public Word(string content, Language language, ContentType contentType) : this()
        {
            Content = content;
            Language = language;
            ContentType = contentType;
        }

        public string Guid { get; private set; }

        public string Content { get; set; }
        public Language Language { get; set; }
        public ContentType ContentType { get; set; }


        public int CompareTo(Word other)
        {
            throw new NotImplementedException();
        }
    }

    public enum ContentType
    {
        Required,
        Display,
        TypeError

    }
    public enum Language
    {
        US_EN,
        IR_FA
    }


}


