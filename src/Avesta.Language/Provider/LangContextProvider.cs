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
        public abstract Task<bool> IsKeyExist(object key, LanguageShortName language);
        public abstract Task<string> Read(object key, LanguageShortName lang);
        public abstract Task<string> ReadText(GlobalWord globalWord, LanguageShortName lang);
        public abstract ValueTask DisposeAsync();

    }
}
