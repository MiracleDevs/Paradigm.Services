using System;
using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.Services.CLI
{
    /// <summary>
    /// Provides an interface for a startup application.
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// Configures the services, inject the dependencies.
        /// </summary>
        /// <param name="services">The service collection.</param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Runs the specified program.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        /// <returns></returns>
        void Run(IServiceProvider provider);
    }
}