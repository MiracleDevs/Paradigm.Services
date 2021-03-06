﻿using FluentAssertions;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.ORM.Data.Database;
using Paradigm.ORM.Data.MySql;
using Paradigm.Services.DependencyInjection.Extensions;
using Paradigm.Services.DependencyInjection.Extensions.ORM;
using Paradigm.Services.Exceptions;
using Paradigm.Services.Repositories.UOW;
using Paradigm.Services.Tests.Fixtures.Tests.Extensions;

namespace Paradigm.Services.Tests.Tests.Extensions
{
    [TestFixture]
    public class ServiceCollectionExtensionsTest
    {
        [TestCase]
        public void ShouldRegisterObjects()
        {
            var serviceCollection = new ServiceCollection();
            var entryPoint = typeof(ServiceCollectionExtensionsTest).Assembly;

            serviceCollection.AddScoped<IDatabaseConnector, MySqlDatabaseConnector>();
            serviceCollection.AddDomainObjects(entryPoint);
            serviceCollection.AddDatabaseAccess(entryPoint);
            serviceCollection.AddDatabaseReaderMappers(entryPoint);
            serviceCollection.AddStoredProcedures(entryPoint);
            serviceCollection.AddRepositories(entryPoint);
            serviceCollection.AddProviders(entryPoint);
            serviceCollection.AddWorkingTasks(entryPoint);
            serviceCollection.AddTransactionalWorkingTasks(entryPoint);
            serviceCollection.AddUnitOfWork();
            serviceCollection.AddExceptionHandler(typeof(Fixtures.Tests.Extensions.Exceptions));

            serviceCollection.Count.Should().Be(21);
        }

        [TestCase]
        public void ShouldResolveObjects()
        {
            var serviceCollection = new ServiceCollection();
            var entryPoint = typeof(ServiceCollectionExtensionsTest).Assembly;

            serviceCollection.AddScoped<IDatabaseConnector, MySqlDatabaseConnector>();
            serviceCollection.AddDomainObjects(entryPoint);
            serviceCollection.AddDatabaseAccess(entryPoint);
            serviceCollection.AddDatabaseReaderMappers(entryPoint);
            serviceCollection.AddStoredProcedures(entryPoint);
            serviceCollection.AddRepositories(entryPoint);
            serviceCollection.AddProviders(entryPoint);
            serviceCollection.AddWorkingTasks(entryPoint);
            serviceCollection.AddUnitOfWork();
            serviceCollection.AddExceptionHandler(typeof(Fixtures.Tests.Extensions.Exceptions));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var domainObject = serviceProvider.GetService<DomainObject1>();
            domainObject.Should().NotBeNull();

            var databaseAccess = serviceProvider.GetService<DomainObject1DatabaseAccess>();
            databaseAccess.Should().NotBeNull();
            var idatabaseAccess = serviceProvider.GetService<IDomainObject1DatabaseAccess>();
            idatabaseAccess.Should().NotBeNull();

            var readerMapper = serviceProvider.GetService<DomainObject1DatabaseReaderMapper>();
            readerMapper.Should().NotBeNull();
            var ireaderMapper = serviceProvider.GetService<IDomainObject1DatabaseReaderMapper>();
            ireaderMapper.Should().NotBeNull();

            var spParameters = serviceProvider.GetService<GetDomainObject1Parameters>();
            spParameters.Should().NotBeNull();

            var storedProcedure = serviceProvider.GetService<GetDomainObject1StoredProcedure>();
            storedProcedure.Should().NotBeNull();
            var istoredProcedure = serviceProvider.GetService<IGetDomainObject1StoredProcedure>();
            istoredProcedure.Should().NotBeNull();

            var repository = serviceProvider.GetService<DomainObject1Repository>();
            repository.Should().NotBeNull();
            var irepository = serviceProvider.GetService<IDomainObject1Repository>();
            irepository.Should().NotBeNull();

            var provider = serviceProvider.GetService<DomainObject1Provider>();
            provider.Should().NotBeNull();
            var iprovider = serviceProvider.GetService<IDomainObject1Provider>();
            iprovider.Should().NotBeNull();

            var iunitOfWork = serviceProvider.GetService<IUnitOfWork>();
            iunitOfWork.Should().NotBeNull();

            var iexceptionHandler = serviceProvider.GetService<IExceptionHandler>();
            iexceptionHandler.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldResolveObjectsWithParadigmFramework()
        {
            var serviceCollection = new ServiceCollection();
            var entryPoint = typeof(ServiceCollectionExtensionsTest).Assembly;

            serviceCollection.AddScoped<IDatabaseConnector, MySqlDatabaseConnector>();
            serviceCollection.AddParadimFramework(typeof(Fixtures.Tests.Extensions.Exceptions), entryPoint);

            serviceCollection.Count.Should().Be(21);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var domainObject = serviceProvider.GetService<DomainObject1>();
            domainObject.Should().NotBeNull();

            var databaseAccess = serviceProvider.GetService<DomainObject1DatabaseAccess>();
            databaseAccess.Should().NotBeNull();
            var idatabaseAccess = serviceProvider.GetService<IDomainObject1DatabaseAccess>();
            idatabaseAccess.Should().NotBeNull();

            var readerMapper = serviceProvider.GetService<DomainObject1DatabaseReaderMapper>();
            readerMapper.Should().NotBeNull();
            var ireaderMapper = serviceProvider.GetService<IDomainObject1DatabaseReaderMapper>();
            ireaderMapper.Should().NotBeNull();

            var spParameters = serviceProvider.GetService<GetDomainObject1Parameters>();
            spParameters.Should().NotBeNull();

            var storedProcedure = serviceProvider.GetService<GetDomainObject1StoredProcedure>();
            storedProcedure.Should().NotBeNull();
            var istoredProcedure = serviceProvider.GetService<IGetDomainObject1StoredProcedure>();
            istoredProcedure.Should().NotBeNull();

            var repository = serviceProvider.GetService<DomainObject1Repository>();
            repository.Should().NotBeNull();
            var irepository = serviceProvider.GetService<IDomainObject1Repository>();
            irepository.Should().NotBeNull();

            var provider = serviceProvider.GetService<DomainObject1Provider>();
            provider.Should().NotBeNull();
            var iprovider = serviceProvider.GetService<IDomainObject1Provider>();
            iprovider.Should().NotBeNull();

            var iunitOfWork = serviceProvider.GetService<IUnitOfWork>();
            iunitOfWork.Should().NotBeNull();
        }
    }
}