using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.ORM.Data.Database;

namespace Paradigm.Services.Mvc.ORM.Middlewares
{
    public class OrmDatabaseMiddleware : IMiddleware
    {
        #region Properties

        private IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructor

        public OrmDatabaseMiddleware(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        #endregion

        #region Public Methods

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var configuration = this.ServiceProvider.GetService<IConfigurationRoot>();
            var connector = this.ServiceProvider.GetService<IDatabaseConnector>();
            connector.Initialize(configuration["Database:ConnectionString"]);

            await connector.OpenAsync();
            await next.Invoke(context);
            await connector.CloseAsync();
        }

        #endregion
    }
}