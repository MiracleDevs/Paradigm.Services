using Paradigm.Services.Providers;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    public interface IDomainObject1Provider: IEditProvider<IDomainObject1, DomainObject1, int>
    {
    }
}