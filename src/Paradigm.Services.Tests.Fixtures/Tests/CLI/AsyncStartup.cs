using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
{
    public class AsyncStartup
    {
        public IConfiguration Configuration { get; }

        public IServiceCollection ServiceCollection { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public AsyncStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            this.ServiceCollection = serviceCollection;
        }

        public async Task Run(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            await Task.Delay(1);
        }
    }
}