/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using Microsoft.AspNetCore.Builder;
using Paradigm.Services.Mvc.Middlewares;

namespace Paradigm.Services.Mvc.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="IApplicationBuilder"/> that helps
    /// with the configuration of paradigm middlewares.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the paradigm exception handler middleware.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseParadigmExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        /// <summary>
        /// Uses the unit of work middleware.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseUnitOfWork(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnitOfWorkMiddleware>();
        }

        /// <summary>
        /// Uses the logging middleware.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseLogs(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogMiddleware>();
        }
    }
}