/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Mvc.Middlewares
{
    public class UnitOfWorkMiddleware : MiddlewareBase
    {
        #region Constructor

        public UnitOfWorkMiddleware(RequestDelegate next): base(next)
        {
        }

        #endregion

        #region Public Methods

        public override async Task Invoke(HttpContext context)
        {
            var unitOfWork = context.RequestServices.GetService<IUnitOfWork>();

            await this.Next.Invoke(context);

            unitOfWork.Reset();
        }

        #endregion
    }
}