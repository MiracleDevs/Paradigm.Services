using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Core.Logging;

namespace Paradigm.Services.Mvc.Middlewares
{
    public class LogMiddleware : IMiddleware
    {
        #region Properties

        private IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructor

        public LogMiddleware(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        #endregion

        #region Public Methods

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var start = DateTime.Now;
            await next.Invoke(context);
            var end = DateTime.Now;

            await LogAsync(end.Subtract(start).TotalMilliseconds, context, this.ServiceProvider);
        }

        #endregion

        #region Private Methods    

        private static async Task LogAsync(double elapsed, HttpContext context, IServiceProvider serviceProvider)
        {
            try
            {
                var logging = serviceProvider.GetService<ILogging>();

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