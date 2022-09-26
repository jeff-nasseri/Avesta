using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Avesta.Share.Extensions
{

    public static class PublicExtension
    {
        public static string ToGoogleCaltureSchema(this string schemaStr)
        {
            return schemaStr.Replace("type", "@type").Replace("context", "@context");
        }
        public static string OrDefault(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return "-";
            return str;
        }
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }

        public static string SetUrlStrStandard(this string url)
        {
            while (true)
            {
                if (!url.Contains("{"))
                    break;

                var sin = url.IndexOf("{");
                var ein = url.IndexOf("}");
                url = url.Remove(sin, ein - sin + 1);
            }
            url = url.Replace("/", "");
            return url;
        }
        public static void ForEach<T>(this IEnumerable<T> data, Action<T> action)
        {
            foreach (var item in data)
            {
                action(item);
            }

        }
        public static void ForEach(this int num, Action f)
        {
            for (int i = 0; i < num; i++)
            {
                f.Invoke();
            }
        }

        public static string ToEnglish(this string persianStr)
        {
            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9',
                ['0'] = '0',
                ['1'] = '1',
                ['2'] = '2',
                ['3'] = '3',
                ['4'] = '4',
                ['5'] = '5',
                ['6'] = '6',
                ['7'] = '7',
                ['8'] = '8',
                ['9'] = '9'
            };
            foreach (var item in persianStr)
            {
                persianStr = persianStr.Replace(item, LettersDictionary[item]);
            }
            return persianStr;
        }
        public async static Task<IEnumerable<Out>> ForEach<T, Out>(this IEnumerable<T> data, Func<T, Task<Out>> func)
        {
            List<Out> list = new List<Out>();
            foreach (var item in data)
            {
                var result = await func(item);
                list.Add(result);
            }
            return list;
        }

        public static string RemoveHTMLTags(this string html)
        {
            Regex htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

            html = htmlRegex.Replace(html, string.Empty);

            return html.Replace("&nbsp;", "");
        }

        public static string SafeReplace(this string str, string oldStr, string replaceStr)
        {
            var result = str.Replace(oldStr, replaceStr ?? string.Empty);
            return result;
        }


        public static string SplitPriceInEachNDigit<T>(this T price, int n = 4) where T : IComparable
        {
            var str = price.ToString();
            var len = str.Length;
            var counter = 0;
            for (int i = len - 1; len >= 0; len--)
            {
                counter++;
                if (counter == n + 1)
                {
                    counter = 0;
                    var result = str.Substring(0, len) + "," + str.Substring(len);
                    str = result;
                    len++;
                }
            }

            if (str[0] == ',')
            {
                str = str.Remove(0, 1);
            }

            return str;
        }

        public static string GetStrOfPriceIRT(this int price)
        {
            return string.Empty;
        }

        public static double GetSecendsFromString(this string str)
        {
            var timeSpan = str.Split(" ");
            var _1 = timeSpan[1];
            var _2 = Int32.Parse(timeSpan[0]);
            Func<int> f = () =>
            {
                switch (_1)
                {
                    case string a when a.Contains("روز"): return 1;
                    case string b when b.Contains("ماه"): return 1 * 30;
                    case string c when c.Contains("سال"): return 1 * 30 * 12;
                    default: return 0;
                }
            };
            var days = f() * _2;
            var result = TimeSpan.FromSeconds(days * 24 * 3600);
            return result.TotalSeconds;
        }
    }
}
