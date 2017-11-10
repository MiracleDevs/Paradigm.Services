using Paradigm.Services.Interfaces;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    internal interface IDomainObject2 : IDomainInterface
    {
        int Id { get; }

        string Name { get; }
    }
}