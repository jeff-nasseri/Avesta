using Avesta.Global;
using System;
using System.Globalization;

namespace Avesta.Language
{
    public class LanguageInfo
    {
        #region Properties

        public string Path { get; }

        public string FileName { get; }

        public string Code { get; }

        public string EnglishName { get; }

        public string NativeName { get; }

        public CultureInfo Culture => new CultureInfo(Code);
        #endregion

        #region Methods

        public void Load()
        {
            Lang.Load(this);
        }

        public override string ToString()
        {
            return EnglishName.Equals(NativeName, StringComparison.OrdinalIgnoreCase) ? EnglishName : $"{NativeName} | {EnglishName}";
        }

        #endregion

        #region Constructors

        public LanguageInfo(string path)
        {
            Path = path;
            Lang.Load(Path, true);
            EnglishName = Lang.EnglishName;
            NativeName = Lang.NativeName;
            Code = FileName = System.IO.Path.GetFileNameWithoutExtension(path);
        }
        #endregion
    }

    public class LanguageRepository : EnhancedDictionary<string, LanguageInfo>
    {
        #region Methods
        public void Add(LanguageInfo info)
        {
            Add(info.FileName, info);
        }
        #endregion
    }
}
