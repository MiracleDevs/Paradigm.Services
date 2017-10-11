/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

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
            app.UseMiddleware<UnitOfWorkMiddleware>();
        }

        public static void UseLogs(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogMiddleware>();
        }
    }
}