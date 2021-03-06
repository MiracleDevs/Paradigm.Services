﻿/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.ORM.Data.Database;
using Paradigm.Services.Mvc.Middlewares;

namespace Paradigm.Services.Mvc.ORM.Middlewares
{
    public class OrmDatabaseMiddleware : MiddlewareBase
    {
        #region Constructor

        public OrmDatabaseMiddleware(RequestDelegate next): base(next)
        {
        }

        #endregion

        #region Public Methods

        public override async Task Invoke(HttpContext context)
        {
            var configuration = context.RequestServices.GetService<IConfiguration>() ?? throw new Exception("Can not resolve a configuration object. Paradigm ORM requires a connection string.");
            var connector = context.RequestServices.GetService<IDatabaseConnector>() ?? throw new Exception("Can not resolve the database connector. Paradigm ORM can not connect.");

            connector.Initialize(configuration["Database:ConnectionString"] ?? throw new Exception("Can not find 'Database:ConnectionString' in the configuration object."));

            await connector.OpenAsync();
            await this.Next.Invoke(context);
            await connector.CloseAsync();
        }

        #endregion
    }
}