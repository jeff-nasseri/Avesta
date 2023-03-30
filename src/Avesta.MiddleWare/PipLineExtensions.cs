using Avesta.Exceptions;
using Avesta.Language;
using Avesta.Model;
using Avesta.Model.Shema;
using Avesta.Share.Model.API;
using Avesta.Constant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Net;
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






        public static IApplicationBuilder SetErrorHandlingForAPI<TWordContext>(this IApplicationBuilder app) where TWordContext : WordContext
        => app.UseExceptionHandler(errorapp =>
        {
            errorapp.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/html";
                var data = context.Features.Get<IExceptionHandlerPathFeature>();
                var wordContext = context.RequestServices.GetService(typeof(TWordContext)) as TWordContext;
                ErrorModel error = default(ErrorModel);
                if (data.Error is SystemException)
                {
                    var finlibException = (data?.Error as SystemException);

                    error = new ErrorModel
                    {
                        Code = finlibException.Code,
                        Message = await wordContext.GetMessageInCurrentActiveLanguage(finlibException.Code)
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
                    StatusNumber = ResponseModel.Status.Fail,
                    Successfull = false
                };

                var json = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(json);
            });
        });

    }




}