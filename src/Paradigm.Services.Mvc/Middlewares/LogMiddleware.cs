/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Core.Logging;

namespace Paradigm.Services.Mvc.Middlewares
{
    public class LogMiddleware : MiddlewareBase
    {
        #region Constructor

        public LogMiddleware(RequestDelegate next): base(next)
        {
        }

        #endregion

        #region Public Methods

        public override async Task Invoke(HttpContext context)
        {
            var start = DateTime.Now;
            await this.Next.Invoke(context);
            var end = DateTime.Now;

            await LogAsync(end.Subtract(start).TotalMilliseconds, context);
        }

        #endregion

        #region Private Methods    

        private static async Task LogAsync(double elapsed, HttpContext context)
        {
            try
            {
                var logging = context.RequestServices.GetService<ILogging>();

                if (logging != null)
                {
                    await logging.LogAsync($"[{elapsed}ms] - Incoming request to {context.Request.Path.Value} from IPv4='{context.Connection.RemoteIpAddress.MapToIPv4()}' IPv6='{context.Connection.RemoteIpAddress.MapToIPv6()}'");
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