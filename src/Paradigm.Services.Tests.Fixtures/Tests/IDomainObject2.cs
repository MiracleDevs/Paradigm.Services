using Paradigm.Services.Interfaces;

namespace Paradigm.Services.Tests.Fixtures.Tests
{
    internal interface IDomainObject2 : IDomainInterface
    {
        int Id { get; }

        string Name { get; }
    }
}