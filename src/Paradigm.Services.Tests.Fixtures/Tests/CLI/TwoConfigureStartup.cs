using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
{
    public class TwoConfigureStartup
    {
        public IConfiguration Configuration { get; }

        public IServiceCollection ServiceCollection { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public TwoConfigureStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            this.ServiceCollection = serviceCollection;
        }

        public void ConfigureServices(IServiceCollection serviceCollection, object a)
        {
            this.ServiceCollection = serviceCollection;
        }
    }
}