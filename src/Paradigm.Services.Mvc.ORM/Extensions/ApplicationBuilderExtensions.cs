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