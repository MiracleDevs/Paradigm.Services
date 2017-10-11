/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Paradigm.Services.Mvc.Middlewares
{
    public interface IMiddleware
    {
        Task Invoke(HttpContext context);
    }
}