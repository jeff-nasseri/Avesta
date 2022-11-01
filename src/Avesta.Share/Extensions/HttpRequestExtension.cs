using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Extensions
{

    public static class HttpContextExtensions
    {


        public static bool IsAjaxRequest(this HttpContext context)
        {

            const string RequestedWithHeader = "X-Requested-With";
            const string XmlHttpRequest = "XMLHttpRequest";

            var request = context.Request;

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Headers != null)
            {
                return request.Headers[RequestedWithHeader] == XmlHttpRequest;
            }

            return false;
        }



        public async static Task<string?> GetHeaderValue(this HttpContext context, string key, string prefix = null)
        {
            await Task.CompletedTask;

            var value = context.Request.Headers[key].ToString();
            if (!string.IsNullOrEmpty(value))
            {
                var result = value.Replace(prefix, string.Empty);
                return result;
            }
            return value;
        }



    }
}
