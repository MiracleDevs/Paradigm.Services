using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Exceptions;
using Paradigm.Services.Repositories.UOW;
using Newtonsoft.Json;

namespace Paradigm.Services.Mvc.Middlewares
{
    public class UnitOfWorkMiddleware : IMiddleware
    {
        #region Properties

        public RequestDelegate Next { get; }

        #endregion

        #region Constructor

        public UnitOfWorkMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var unitOfWork = serviceProvider.GetService<IUnitOfWork>();

            await this.Next.Invoke(context);
            
            unitOfWork.Reset();
        }

        #endregion
    }
}