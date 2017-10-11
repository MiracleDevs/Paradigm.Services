using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Mvc.Middlewares
{
    public class UnitOfWorkMiddleware : IMiddleware
    {
        #region Properties

        private IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructor

        public UnitOfWorkMiddleware(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        #endregion

        #region Public Methods

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var unitOfWork = this.ServiceProvider.GetService<IUnitOfWork>();

            await next.Invoke(context);

            unitOfWork.Reset();
        }

        #endregion
    }
}