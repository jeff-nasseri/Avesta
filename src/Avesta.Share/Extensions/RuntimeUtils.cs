using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysEnum = System.Enum;

namespace Avesta.Share.Extensions
{
    public static class RuntimeExtensions
    {
        public static IEnumerable<T> GetListOfValues<T>(this Type enumType)
        {
            Type enumUnderlyingType = SysEnum.GetUnderlyingType(enumType);
            Array enumValues = SysEnum.GetValues(enumType);

            for (int i = 0; i < enumValues.Length; i++)
            {
                // Retrieve the value of the ith enum item.
                object value = enumValues.GetValue(i);

                // Convert the value to its underlying type (int, byte, long, ...)
                object underlyingValue = System.Convert.ChangeType(value, enumUnderlyingType);

                yield return (T)Convert.ChangeType(underlyingValue, typeof(T));
            }

        }
    }
}
