/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

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
    public class ExceptionHandlerMiddleware : MiddlewareBase
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


        #region Constructor

        public ExceptionHandlerMiddleware(RequestDelegate next): base(next)
        {
        }

        #endregion

        #region Public Methods

        public override async Task Invoke(HttpContext context)
        {
            try
            {
                await this.Next(context);
            }
            catch (Exception ex)
            {
                await LogAsync(context.RequestServices, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        #endregion

        #region Private Methods

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case AuthenticationException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case AuthorizationException _:
                    code = HttpStatusCode.Forbidden;
                    break;
            }

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
                    await logging.LogAsync(ex.Message.Replace(Environment.NewLine, "<br />"), LogType.Error, ex.StackTrace.Replace(Environment.NewLine, "<br />"));
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