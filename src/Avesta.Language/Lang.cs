using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using Avesta.Storage.Constant;
using Avesta.Global;

namespace Avesta.Language
{

    [Flags]
    public enum TranslationOptions
    {
        ConvertDigits = 1,
    }

    [Flags]
    public enum LangLoadFlags
    {
        None = 0,

        InfoOnly,

        AppendOnly,

        ExtensionLoad
    }

    class LangExtension
    {
        #region Properties
        public string RootPath { get; set; }

        public string DefaultLangCode { get; set; }
        #endregion

        #region Constructors
        public LangExtension() { }

        public LangExtension(string root_path, string default_lang_code = null)
        {
            RootPath = root_path;
            DefaultLangCode = default_lang_code;
        }
        #endregion

        #region Standard Methods
        public override int GetHashCode()
        {
            return RootPath.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var e = obj as LangExtension;
            if (e == null)
                return false;
            return e.RootPath.Equals(RootPath, StringComparison.OrdinalIgnoreCase);
        }
        #endregion
    }

    public static class Lang
    {

        #region initilized function
        public static void LoadLanguages()
        {
            LanguageRepository repository = Lang.Repository;
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Language");
            var files = Directory.GetFiles(path).Where(f => f.EndsWith("lang")).ToList();
            foreach (var file in files)
                repository.Add(new LanguageInfo(file));
            repository[Storage.Constant.Language.DefaultLanguagePrefix].Load();
        }
        #endregion


        #region Constants + Modifiers Map

        private static readonly EnhancedDictionary<char, IStringModifier> ModifiersMap =
            new EnhancedDictionary<char, IStringModifier>()
            {
                {'U', new StringCaseModifier(true)},
                {'L', new StringCaseModifier(false)},
            };
        #endregion

        #region Inner Definitions
        private delegate void LanguageCommandAction(string[] arr, CommandArgumentMap map);
        private class CommandArgumentMap : EnhancedDictionary<string, string[]> { }
        public class LangKey
        {
            public string Key;
            public IStringModifier[] Modifiers;
            public override string ToString()
            {
                if (Modifiers == null || Modifiers.Length == 0)
                    return Key;
                var sb = new StringBuilder(Key);
                sb.Append("/");
                foreach (var m in Modifiers)
                    switch (m)
                    {
                        case StringCaseModifier scm:
                            sb.Append("i");
                            break;
                        default:
                            throw new NotImplementedException("Unknown string modifier type.");
                    }
                return sb.ToString();
            }
        }

        private class LanguageCommandMap : EnhancedDictionary<string, LanguageCommandAction> { }
        #endregion

        #region Fields and Properties
        private static LangExpressions Expressions = new LangExpressions();

        private static string[] NativeDigits = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public static LanguageRepository Repository { get; } = new LanguageRepository();
        public static EnhancedDictionary<string, ObservableValue<object>> Settings = new EnhancedDictionary<string, ObservableValue<object>>();

        private static readonly HashSet<LangExtension> Extensions = new HashSet<LangExtension>();

        public static LanguageInfo CurrentLanguage { get; private set; }
        public static string EnglishName { get; private set; }
        public static string NativeName { get; private set; }
        #endregion

        #region Events
        public static event Action Changed;
        #endregion

        #region Initializer
        static Lang()
        {
        }
        #endregion

        #region Internal Methods
        private static LangKey ParseKey(string s)
        {
            s = s.Trim();
            var r = new LangKey
            {
                Key = s,
                Modifiers = new IStringModifier[0]
            };
            var p = r.Key.IndexOf("/", StringComparison.Ordinal);
            if (p >= 0)
            {
                var mod = r.Key.Substring(p + 1);
                var mods = new List<IStringModifier>();
                foreach (char t in mod)
                    if (ModifiersMap.ContainsKey(t))
                        mods.Add(ModifiersMap[t]);
                r.Modifiers = mods.ToArray();
                r.Key = r.Key.Substring(0, p);
            }
            r.Key = r.Key.ToLower();
            return r;
        }
        private static string ParseExpression(string s)
        {
            return s
                .Trim()
                .Replace("\\n", Environment.NewLine);
        }
        #endregion


        #region String Helpers
        public static string ConvertDigitsToNative(this string s)
        {
            for (int i = 0; i < 10; i++)
                s = s.Replace(i.ToString(), NativeDigits[i]);
            return s;
        }
        #endregion



        #region Methods
        public static PreparedLanguage Prepare(Stream s, LangLoadFlags flags = LangLoadFlags.None)
        {
            var entries = new List<PreparedLanguageEntry>();
            var r = new PreparedLanguage(null, entries);
            var sr = new StreamReader(s);
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
                    continue;
                var data = line.Split(new[] { '=' }, 2);
                if (data.Length != 2)
                    continue;
                if (flags.HasFlag(LangLoadFlags.InfoOnly))
                    break;
                var key = ParseKey(data[0]);
                var value = ParseExpression(data[1]);
                entries.Add(new PreparedExpressionEntry { Key = key, Value = value });
            }
            Changed?.Invoke();
            return r;
        }

        public static PreparedLanguage Prepare(string path, LangLoadFlags flags = LangLoadFlags.None)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                return Prepare(fs, flags);
            }
        }

        public static void Load(StreamReader sr, bool clear = false, bool info_only = false, LangLoadFlags flags = LangLoadFlags.None)
        {
            lock (Expressions)
            {
                if (clear)
                    Expressions.Clear();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
                        continue;
                    var data = line.Split(new[] { '=' }, 2);
                    if (data.Length != 2)
                        continue;
                    if (info_only)
                        break;
                    var key = ParseKey(data[0]);
                    var value = ParseExpression(data[1]);
                    var le = Expressions[key.Key];
                    if (le == null)
                    {
                        if (flags.HasFlag(LangLoadFlags.AppendOnly) && Expressions.ContainsKey(key.Key))
                            continue;
                        Expressions[key.Key] = new LangExpression(key.Key, value);
                    }
                    else
                        le.Value = value;
                }
                Changed?.Invoke();
            }
        }

        public static void Load(string path, bool info_only = false, LangLoadFlags flags = LangLoadFlags.None)
        {
            using (var stream = new StreamReader(path))
                Load(stream);
        }

        public static void Load(LanguageInfo lang, bool force = false)
        {
            if (lang == null)
                throw new ArgumentNullException(nameof(lang));
            if (!force && lang.Code == CurrentLanguage?.Code)
                return;
            Load(lang.Path);
            CurrentLanguage = lang;
        }

      
        public static LangExpression Get(string key, bool defaultIfNotExists = true)
        {
            var modifiers = new List<IStringModifier>();
            var k = ParseKey(key);
            var r = Expressions[key.ToLower()] ?? Expressions[k.Key];
            if (defaultIfNotExists && r == null)
                Expressions[key.ToLower()] = r = new LangExpression(key, key ?? k.Key);
            if (k.Modifiers.Length > 0)
            {
                var n = new LangExpression(key, r.Value);
                n.Modifiers.AddRange(k.Modifiers);
                n.DependsOn(r);
                Expressions[key.ToLower()] = n;
                r = n;
            }
            return r;
        }
        public static string Translate(string key, params object[] formats)
        {
            var e = Get(key);
            if (e == null)
                return key;
            return string.Format(e.Value, formats);
        }
        public static string T(string key, params object[] formats)
        {
            return Translate(key, formats);
        }
        public static string T(string key, TranslationOptions options, params object[] formats)
        {
            var r = T(key, formats);
            if (options.HasFlag(TranslationOptions.ConvertDigits))
                r = r.ConvertDigitsToNative();
            return r;
        }

        public static List<LangExpression> GetCurrentExpressions()
        {
            lock (Expressions)
            {
                return new List<LangExpression>(Expressions.Values);
            }
        }
        #endregion
    }
}
