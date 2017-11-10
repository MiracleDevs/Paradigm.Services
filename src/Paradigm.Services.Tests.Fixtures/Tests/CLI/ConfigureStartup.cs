using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
{
    public class ConfigureStartup
    {
        public IConfiguration Configuration { get; }

        public IServiceCollection ServiceCollection { get; private set; }

        public ConfigureStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            this.ServiceCollection = serviceCollection;
        }
    }
}