using System;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Exceptions;
using Newtonsoft.Json;
using Paradigm.Core.Logging;

namespace Paradigm.Services.Mvc.Middlewares
{
    public class ExceptionHandlerMiddleware: IMiddleware
    {
        #region Nested Types

        [DataContract]
        public class Error
        {
            [DataMember]
            public string Message { get; set; }

            public Error(string message)
            {
                this.Message = message;
            }
        }

        #endregion

        #region Properties

        private IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructor

        public ExceptionHandlerMiddleware(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        #endregion

        #region Public Methods

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await LogAsync(this.ServiceProvider, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        #endregion

        #region Private Methods

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is NotFoundException)
                code = HttpStatusCode.NotFound;

            else if (exception is AuthenticationException)
                code = HttpStatusCode.Unauthorized;

            else if (exception is AuthorizationException)
                code = HttpStatusCode.Forbidden;

            var result = JsonConvert.SerializeObject(new Error(exception.Message));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static async Task LogAsync(IServiceProvider serviceProvider, Exception ex)
        {
            try
            {
                var logging = serviceProvider.GetService<ILogging>();

                if (logging != null)
                {
                    await logging.LogAsync(ex.Message, LogType.Error, Environment.NewLine + ex.StackTrace);
                }
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }
}