using Avesta.Language.Globalization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avesta.Globalization.Language
{

    public abstract class LangContextProvider : IAsyncDisposable
    {
        public abstract Task Write(Word word);
        public abstract Task WriteRange(IEnumerable<Word> words);
        public abstract Task Save();
        public abstract Task<Word> Read(string id, LanguageShortName lang);
        public abstract Task<string> ReadText(GlobalWord globalWord, LanguageShortName lang);
        public abstract ValueTask DisposeAsync();

    }

   
}


