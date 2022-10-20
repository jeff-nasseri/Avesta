using Avesta.Exceptions;
using Avesta.Model;
using Avesta.Model.API;
using Avesta.Model.Shema;
using Avesta.Share.Helper;
using Avesta.Storage.Constant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using SystemException = Avesta.Exceptions.SystemException;

namespace Avesta.MiddleWare
{

    public static class PipLineExtensions
    {

        public static IApplicationBuilder SchemaCather<T>(this IApplicationBuilder app) where T : RequestSchema
            => app.UseMiddleware<T>();






        public static IApplicationBuilder SetErrorHandlingForMVC(this IApplicationBuilder app)
            => app.UseExceptionHandler(errorapp =>
            {
                errorapp.Run(async context =>
                {
                    await Task.CompletedTask;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";
                    var data = context.Features.Get<IExceptionHandlerPathFeature>();

                    System.Console.WriteLine($"{data.Error?.Message}\n{data.Error?.StackTrace}");
                    ITempDataDictionaryFactory factory = context.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
                    ITempDataDictionary tempData = factory.GetTempData(context);

                    if (data.Error is SystemException)
                    {
                        var sysException = data.Error as SystemException;
                        tempData[ExceptionKeys.ErrorKey] = ErrorManager.GetErrorMessageByCode(sysException.Code);
                    }
                    context.Response.Redirect($"/account/login");
                });
            });






        public static IApplicationBuilder SetErrorHandlingForAPI(this IApplicationBuilder app)
        => app.UseExceptionHandler(errorapp =>
        {
            errorapp.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/html";
                var data = context.Features.Get<IExceptionHandlerPathFeature>();
                ErrorModel error = default(ErrorModel);
                if (data.Error is SystemException)
                {
                    var finlibException = (data?.Error as SystemException);

                    error = new ErrorModel
                    {
                        Code = finlibException.Code,
                        Message = ErrorManager.GetErrorMessageByCode(finlibException.Code)
                    };
                }
                else
                {
                    error = new ErrorModel
                    {
                        Code = -1,
                        Message = data.Error.Message,
                        Stack = data.Error.StackTrace
                    };
                }
                var response = new ResponseModel
                {
                    Data = error,
                    Message = error.Message,
                    Status = 500,
                    Successfull = false
                };

                var json = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(json);
            });
        });

    }




}