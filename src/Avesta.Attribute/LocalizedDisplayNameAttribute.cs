using Avesta.Language.Globalization.Enum;
using Avesta.Language.Globalization.Model;
using Avesta.Language.Globalization.Provider;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Avesta.Attribute
{

    public class AvestaDisplayNameAttribute : DisplayNameAttribute
    {

        readonly LangContextProvider _langContextProvider;
        public AvestaDisplayNameAttribute(LangContextProvider langContextProvider)
        {
            _langContextProvider = langContextProvider;
        }

        public GlobalWord Word { get; set; }
        public LanguageShortName Lang { get; set; }


        public override string DisplayName => GetWord(Word, Lang).Result;


        async Task<string> GetWord(GlobalWord globalWord, LanguageShortName lang) => await _langContextProvider.ReadText(globalWord, lang);

        public AvestaDisplayNameAttribute(GlobalWord wordList, LanguageShortName lang)
            : base()
        {
            Word = wordList;
            Lang = lang;
        }



    }







    //public class LocalizeDataTypeAttribute : ValidationAttribute
    //{
    //    readonly DataType _type;
    //    public LocalizeDataTypeAttribute(DataType dataType, int messageCode)
    //        : base(errorMessage: LocalStorage.GetMessageFromResource(messageCode))
    //    {
    //        _type = dataType;
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        switch (_type)
    //        {
    //            case DataType.EmailAddress:
    //                return IsEmail(value);
    //            default: throw new Exception($"DataType : {_type} ---> not found in LocalizeDataTypeAttribute function");
    //        }
    //    }

    //    public static bool IsEmail(object value)
    //    {
    //        return true;
    //    }
    //}
    //public class LocalizeRequiredAttribute : ValidationAttribute
    //{
    //    public LocalizeRequiredAttribute(int messageCode)
    //        : base(errorMessage: LocalStorage.GetMessageFromResource(messageCode))
    //    {
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        if (value == null || string.IsNullOrEmpty(value.ToString()))
    //            return false;
    //        return true;
    //    }
    //}







}
