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

        public RequestDelegate Next { get; }

        #endregion

        #region Constructor

        public LogMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var start = DateTime.Now;
            await this.Next.Invoke(context);
            var end = DateTime.Now;

            await LogAsync(end.Subtract(start).TotalMilliseconds, context, serviceProvider);
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
                    await logging.LogAsync($"[{elapsed}ms] - Incoming request to {context.Request.Path.Value}");
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