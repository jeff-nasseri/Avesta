using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Avesta.Share.Extensions
{
    public static class UrlExtension
    {
        public static string RemoveQueryStringByKey(this string query, string key)
        {
            var qs = HttpUtility.ParseQueryString(query);
            qs.Remove(key);
            var newQuerystring = qs.ToString();
            return newQuerystring;
        }
    }




}
