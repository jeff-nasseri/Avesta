using Avesta.Language.Globalization.Enum;
using Avesta.Language.Globalization.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avesta.Language.Globalization.Provider
{
    public abstract class LangContextProvider : IAsyncDisposable
    {
        public abstract Task Write(GlobalWord globalWord);
        public abstract Task Save();
        public abstract Task<Word> Read(string id, LanguageShortName lang);
        public abstract Task<string> ReadText(GlobalWord globalWord, LanguageShortName lang);
        public abstract ValueTask DisposeAsync();

    }
}
