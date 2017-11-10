using Microsoft.Extensions.Configuration;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
{
    public class ConstructorStartup
    {
        public IConfiguration Configuration { get; }

        public ConstructorStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
    }
}