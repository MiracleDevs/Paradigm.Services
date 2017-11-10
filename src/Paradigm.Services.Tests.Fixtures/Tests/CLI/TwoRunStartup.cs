using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
{
    public class TwoRunStartup
    {
        public IConfiguration Configuration { get; }

        public IServiceCollection ServiceCollection { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public TwoRunStartup(IConfiguration configuration)
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

        public async Task Run(IServiceProvider serviceProvider, object other)
        {
            this.ServiceProvider = serviceProvider;
            await Task.Delay(1);
        }
    }
}