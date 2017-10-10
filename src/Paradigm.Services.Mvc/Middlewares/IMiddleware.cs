using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Paradigm.Services.Mvc.Middlewares
{
    public interface IMiddleware
    {
        RequestDelegate Next { get; }

        Task Invoke(HttpContext context, IServiceProvider serviceProvider);
    }
}