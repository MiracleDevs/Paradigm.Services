using Paradigm.Services.Interfaces;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    internal interface IDomainObject2 : IDomainInterface
    {
        int Id { get; }

        string Name { get; }
    }
}