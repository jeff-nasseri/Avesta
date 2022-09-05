using Avesta.Language.Globalization;
using Microsoft.Extensions.DependencyInjection;
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
        public abstract Task<Word> Read(string id, LanguageShortName lang);
        public abstract ValueTask DisposeAsync();

    }

   





    //public class TestWordContext : WordContext
    //{

    //    public static WordList Username = new WordList(new Word("username", Language.US_EN, ContentType.Display));

    //}





}


