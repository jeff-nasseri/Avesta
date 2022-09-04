using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Utilities
{
    public static class NavigationManagerExtensions
    {
        public static T GetQueryString<T>(this NavigationManager navManager, string key)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    return (T)(object)valueAsInt;
                }

                if (typeof(T) == typeof(string))
                {
                    return (T)(object)valueFromQueryString.ToString();
                    
                }

                if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
                {
                    return (T)(object)valueAsDecimal;
                }
            }
            return default(T);
        }
    }

}
