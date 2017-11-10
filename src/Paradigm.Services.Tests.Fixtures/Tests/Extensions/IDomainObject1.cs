using Paradigm.Services.Interfaces;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    public interface IDomainObject1: IDomainInterface
    {
        int Id { get; }

        string Name { get; }
    }
}