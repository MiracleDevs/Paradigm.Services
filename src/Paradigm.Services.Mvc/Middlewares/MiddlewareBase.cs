/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Paradigm.Services.Mvc.Middlewares
{
    public abstract class MiddlewareBase: IMiddleware
    {
        #region Properties

        protected RequestDelegate Next { get; }

        #endregion

        #region Constructor

        protected MiddlewareBase(RequestDelegate next)
        {
            this.Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        #endregion

        #region Public Methods

        public abstract Task Invoke(HttpContext context);

        #endregion
    }
}