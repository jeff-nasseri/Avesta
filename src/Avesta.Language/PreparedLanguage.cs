using System;
using System.Collections.Generic;
using System.Text;

namespace Avesta.Language
{
    public interface PreparedLanguageEntry
    {
    }

    public class PreparedCommandEntry : PreparedLanguageEntry
    {
        public string[] Parts { get; set; }
    }

    public class PreparedExpressionEntry : PreparedLanguageEntry
    {
        public Lang.LangKey Key { get; set; }

        public string Value { get; set; }
    }

    public class PreparedLanguage
    {
        #region Properties
        public LanguageInfo Info { get; set; }

        public List<PreparedLanguageEntry> Entries { get; set; }
        #endregion

        #region Constructor
        public PreparedLanguage() { }
        public PreparedLanguage(LanguageInfo info, IEnumerable<PreparedLanguageEntry> entries)
        {
            Info = info;
            Entries = entries as List<PreparedLanguageEntry> ?? new List<PreparedLanguageEntry>(entries);
        }
        #endregion
    }
}
