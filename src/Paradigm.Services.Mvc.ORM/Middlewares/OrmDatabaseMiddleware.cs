using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Mvc.Middlewares;
using Paradigm.ORM.Data.Database;

namespace Paradigm.Services.Mvc.ORM.Middlewares
{
    public class OrmDatabaseMiddleware : IMiddleware
    {
        #region Properties

        public RequestDelegate Next { get; }

        #endregion

        #region Constructor

        public OrmDatabaseMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfigurationRoot>();
            var connector = serviceProvider.GetService<IDatabaseConnector>();
            connector.Initialize(configuration["Database:ConnectionString"]);

            await connector.OpenAsync();
            await this.Next.Invoke(context);
            await connector.CloseAsync();

        }

        #endregion
    }
}