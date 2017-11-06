using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.ORM.Data.Attributes;
using Paradigm.ORM.Data.DatabaseAccess;
using Paradigm.ORM.Data.Mappers;
using Paradigm.ORM.Data.StoredProcedures;
using Paradigm.Services.WorkingTasks;
using Paradigm.Services.WorkingTasks.ORM;

namespace Paradigm.Services.DependencyInjection.Extensions.ORM
{
    /// <summary>
    /// Provides automatic registration extensions for the service provider.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all the different layers used by the paradigm framework.
        /// </summary>
        /// <remarks>
        /// This method will register:
        /// - Domain objects and views.
        /// - Database access objects.
        /// - Database reader mappers.
        /// - Stored procedures and stored procedure parameters.
        /// - Repositories.
        /// - Providers.
        /// - Working Tasks.
        /// </remarks>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="exceptionHandlerResourceType">Resource type with exception messages.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddParadimFramework(this IServiceCollection serviceCollection, Type exceptionHandlerResourceType, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddDomainObjects(assembly);
            serviceCollection.AddDatabaseAccess(assembly);
            serviceCollection.AddDatabaseReaderMappers(assembly);
            serviceCollection.AddStoredProcedures(assembly);
            serviceCollection.AddRepositories(assembly);
            serviceCollection.AddProviders(assembly);
            serviceCollection.AddWorkingTasks(assembly);
            serviceCollection.AddTransactionalWorkingTasks(assembly);
            serviceCollection.AddUnitOfWork();
            serviceCollection.AddExceptionHandler(exceptionHandlerResourceType);
        }

        /// <summary>
        /// Registers the working tasks.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddTransactionalWorkingTasks(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddTransient<ITransactionalWorkTask, TransactionalWorkTask>();
            var registrableTypes = GetTypesThatInherit(typeof(ITransactionalWorkTask), assembly);

            foreach (var type in registrableTypes)
            {
                var interfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault(x => typeof(ITransactionalWorkTask).IsAssignableFrom(x));

                if (interfaceType == null)
                    continue;

                serviceCollection.AddTransient(interfaceType, type);
                serviceCollection.AddTransient(type);
            }
        }

        /// <summary>
        /// Registers the database access classes.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddDatabaseAccess(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var registrableTypes = GetTypesThatInherit(typeof(IDatabaseAccess), assembly);

            foreach (var type in registrableTypes)
            {
                var interfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault(x => typeof(IDatabaseAccess).IsAssignableFrom(x));
                var genericInterfaceType = interfaceType?.GetInterfaces().Where(x => x.GetGenericArguments() != null && x.GetGenericArguments().Any()).FirstOrDefault(x => typeof(IDatabaseAccess).IsAssignableFrom(x));

                if (interfaceType == null || genericInterfaceType == null)
                    continue;

                serviceCollection.AddTransient(interfaceType, type);
                serviceCollection.AddTransient(genericInterfaceType, type);
                serviceCollection.AddTransient(type);
            }
        }

        /// <summary>
        /// Registers the database reader mappers.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddDatabaseReaderMappers(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var registrableTypes = GetTypesThatInherit(typeof(IDatabaseReaderMapper), assembly);

            foreach (var type in registrableTypes)
            {
                var interfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault(x => typeof(IDatabaseReaderMapper).IsAssignableFrom(x));
                var genericInterfaceType = interfaceType?.GetInterfaces().Where(x => x.GetGenericArguments() != null && x.GetGenericArguments().Any()).FirstOrDefault(x => typeof(IDatabaseReaderMapper).IsAssignableFrom(x));

                if (interfaceType == null || genericInterfaceType == null)
                    continue;

                serviceCollection.AddTransient(interfaceType, type);
                serviceCollection.AddTransient(genericInterfaceType, type);
                serviceCollection.AddTransient(type);
            }
        }

        /// <summary>
        /// Registers the stored procedures.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddStoredProcedures(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var registrableTypes = GetTypesThatInherit(typeof(IRoutine), assembly);

            foreach (var type in registrableTypes)
            {
                var interfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault(x => typeof(IRoutine).IsAssignableFrom(x));

                if (interfaceType == null)
                    continue;

                serviceCollection.AddTransient(interfaceType, type);
                serviceCollection.AddTransient(type);
            }

            RegisterTypesThatAreDecoratedBy(serviceCollection, typeof(RoutineAttribute), assembly);
        }

        /// <summary>
        /// Registers the types.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="type">The type.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        /// <exception cref="ArgumentNullException">serviceCollection</exception>
        private static void RegisterTypesThatAreDecoratedBy(IServiceCollection serviceCollection, Type type, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var registrableTypes = GetTypesThatAreDecoratedBy(type, assembly);

            foreach (var registrableType in registrableTypes)
            {
                serviceCollection.AddTransient(registrableType);
            }
        }

        /// <summary>
        /// Gets the types that inherit.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        /// <returns></returns>
        private static List<TypeInfo> GetTypesThatInherit(Type type, Assembly assembly = null)
        {
            // TODO: move this method as an extension of assenbly to Paradigm.Services.Extensions
            return (assembly ?? Assembly.GetCallingAssembly())
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(x => type.IsAssignableFrom(x.AsType()) &&
                            !x.IsAbstract &&
                            !x.IsInterface &&
                            x.IsPublic &&
                            !x.Namespace.StartsWith($"{nameof(Paradigm)}.{nameof(ORM)}"))
                .ToList();
        }

        /// <summary>
        /// Gets the types that inherit.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        /// <returns></returns>
        private static List<TypeInfo> GetTypesThatAreDecoratedBy(Type type, Assembly assembly = null)
        {
            // TODO: move this method as an extension of assenbly to Paradigm.Services.Extensions
            return (assembly ?? Assembly.GetEntryAssembly())
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(x => x.GetCustomAttribute(type) != null &&
                            !x.IsAbstract &&
                            !x.IsInterface &&
                            x.IsPublic &&
                            !x.Namespace.StartsWith($"{nameof(Paradigm)}.{nameof(ORM)}"))
                .ToList();
        }
    }
}