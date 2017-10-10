using Microsoft.AspNetCore.Builder;
using Paradigm.Services.Mvc.Middlewares;

namespace Paradigm.Services.Mvc.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseOwnExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public static void UseUnitOfWork(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public static void UseLogs(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogMiddleware>();
        }
    }
}