/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using Microsoft.AspNetCore.Builder;
using Paradigm.Services.Mvc.ORM.Middlewares;

namespace Paradigm.Services.Mvc.ORM.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseParadigmOrm(this IApplicationBuilder app)
        {
            app.UseMiddleware<OrmDatabaseMiddleware>();
        }
    }
}