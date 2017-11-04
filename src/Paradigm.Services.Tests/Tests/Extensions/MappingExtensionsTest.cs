using FluentAssertions;
using NUnit.Framework;
using Paradigm.Core.Mapping;
using Paradigm.Services.Mapping.Extensions;
using Paradigm.Services.Tests.Fixtures.Tests;

namespace Paradigm.Services.Tests.Tests.Extensions
{
    [TestFixture]
    public class MappingExtensionsTest
    {
        [TestCase]
        public void ShouldRegisterMappings()
        {
            var entryPoint = typeof(ServiceCollectionExtensionsTest).Assembly;
            var registerMethod = nameof(DomainObject1.RegisterMapping);

            Mapper.Initialize(MapperLibrary.AutoMapper);
            Mapper.Container.AddMappings(entryPoint, registerMethod);

            Mapper.Container.MapExists(typeof(IDomainObject1), typeof(DomainObject1)).Should().BeTrue();
        }

        [TestCase]
        public void ShouldMapAfterRegistration()
        {
            var entryPoint = typeof(ServiceCollectionExtensionsTest).Assembly;
            var registerMethod = nameof(DomainObject1.RegisterMapping);

            Mapper.Initialize(MapperLibrary.AutoMapper);
            Mapper.Container.AddMappings(entryPoint, registerMethod);

            var view = new DomainObject1View
            {
                Id = 1,
                Name = "Test"
            };

            var domain = new DomainObject1().MapFrom(view);

            domain.Id.Should().Be(view.Id);
            domain.Name.Should().Be(view.Name);
        }
    }
}