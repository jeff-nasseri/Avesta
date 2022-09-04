using Avesta.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute
{
    public static class LocalStorage
    {
        const string DataAnnotationPrefixInLangFile = "annotation.code.";

        public static string GetMessageFromResource(int messageCode)
        {
            var key = $"{DataAnnotationPrefixInLangFile}{messageCode}";
            var result = Lang.T(key);
            return result;
        }
    }

    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {

        public LocalizedDisplayNameAttribute(int messageCode)
            : base(LocalStorage.GetMessageFromResource(messageCode))
        {
        }


    }

    public class LocalizeDataTypeAttribute : ValidationAttribute
    {
        readonly DataType _type;
        public LocalizeDataTypeAttribute(DataType dataType, int messageCode)
            : base(errorMessage: LocalStorage.GetMessageFromResource(messageCode))
        {
            _type = dataType;
        }

        public override bool IsValid(object value)
        {
            switch (_type)
            {
                case DataType.EmailAddress:
                    return IsEmail(value);
                default: throw new Exception($"DataType : {_type} ---> not found in LocalizeDataTypeAttribute function");
            }
        }

        public static bool IsEmail(object value)
        {
            return true;
        }
    }
    public class LocalizeRequiredAttribute : ValidationAttribute
    {
        public LocalizeRequiredAttribute(int messageCode)
            : base(errorMessage: LocalStorage.GetMessageFromResource(messageCode))
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;
            return true;
        }
    }

}
