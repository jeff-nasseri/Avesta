using Avesta.Attribute.Schema;
using Avesta.Model.Shema;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.MiddleWare
{

    public class CatchRequestSchema<T> where T : RequestSchema
    {
        readonly RequestDelegate _next;
        public CatchRequestSchema(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, T schemaModel)
        {

            var request = httpContext.Request;


            var properties = schemaModel.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes<RequestSchemaAttribute>();

                foreach (var attribute in attributes)
                {
                    if (attribute is RequestHeaderAttribute)
                    {
                        var key = (attribute as RequestHeaderAttribute).Key;
                        var headerValue = request.Headers[key].ToString();
                        property.SetValue(headerValue, schemaModel);
                    }
                    if (attribute is RequestCookieAttribute)
                    {
                        var key = (attribute as RequestCookieAttribute).Key;
                        var cookieValue = request.Cookies[key].ToString();
                        property.SetValue(cookieValue, attribute);
                    }
                }

            }



            await _next(httpContext);
        }
    }

}
