using Avesta.Exceptions;
using Avesta.Model;
using Avesta.Model.API;
using Avesta.Storage.Constant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using SystemException = Avesta.Exceptions.SystemException;

namespace Avesta.MiddleWare
{
    public static class PipLineExtensions
    {

        public class Catch<T> where T : class
        {
            readonly RequestDelegate _next;
            readonly CatchModel<T> _catchModel;
            public Catch(RequestDelegate next, CatchModel<T> catchModel)
            {
                _next = next;
                _catchModel = catchModel;
            }

            public async Task InvokeAsync(HttpContext httpContext)
            {




                await _next(httpContext);
            }
        }

        public class CatchModel<T> where T : class
        {



            readonly IHttpContextAccessor _httpContextAccessor;

            public CatchModel(IHttpContextAccessor httpContextAccessor) : base()
            {
                _httpContextAccessor = httpContextAccessor;

            }

            public CatchModel(CatchSource source)
            {
                Source = source;
            }




            public virtual CatchModel<T> Catch(Expression<Func<T, string>> Query)
            {
                return this;
            }
            public virtual CatchModel<T> Catch(string key)
            {



                return this;
            }



            protected virtual CatchModel<T> Switcher(string? key, Expression<Func<T, string>>? query)
            {
                switch (Source)
                {
                    case CatchSource.Header:
                        {
                            var valueStr = _httpContextAccessor.HttpContext.Request.Cookies[key].ToString();
                            Value = (T)Convert.ChangeType(valueStr, typeof(T));
                        }
                        break;
                    case CatchSource.Cookie: break;
                    case CatchSource.Schema: break;
                    default: break;
                }

                throw new NotImplementedException();

            }



            //TODO : Extension method for reading from IEnumerable<KeyValuPair<T,G>>



            public virtual CatchSource Source { get; set; }
            public virtual string Key { get; set; }
            public virtual T Value { get; set; }
            public virtual Expression<Func<T, string>> Query { get; set; }
        }

        public enum CatchSource
        {
            Header,
            Cookie,
            Schema
        }








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