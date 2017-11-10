using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
{
    public class SyncStartup
    {
        public IConfiguration Configuration { get; }

        public IServiceCollection ServiceCollection { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public SyncStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;    
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            this.ServiceCollection = serviceCollection;
        }

        public void Run(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }
    }
}