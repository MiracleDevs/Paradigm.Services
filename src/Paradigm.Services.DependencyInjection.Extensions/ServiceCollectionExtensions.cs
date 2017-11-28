using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Domain;
using Paradigm.Services.Exceptions;
using Paradigm.Services.Providers;
using Paradigm.Services.Repositories;
using Paradigm.Services.Repositories.UOW;
using Paradigm.Services.WorkingTasks;

namespace Paradigm.Services.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides automatic registration extensions for the service provider.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the unit of work scoped to the request lifespan.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public static void AddUnitOfWork(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Registers the exception handler.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="resourceType"></param>
        /// <exception cref="System.ArgumentNullException">serviceCollection</exception>
        public static void AddExceptionHandler(this IServiceCollection serviceCollection, Type resourceType)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddSingleton<IExceptionHandler>(new ExceptionHandler(resourceType));
        }

        /// <summary>
        /// Registers the domain classes.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddDomainObjects(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            RegisterTypes(serviceCollection, typeof(DomainBase), assembly);
        }

        /// <summary>
        /// Registers the repositories.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddRepositories(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var registrableTypes = GetTypesThatInherit(typeof(IRepository), assembly);

            foreach(var type in registrableTypes)
            {
                var interfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault(x => typeof(IRepository).IsAssignableFrom(x));

                if (interfaceType == null)
                    continue;

                serviceCollection.AddTransient(interfaceType, type);
                serviceCollection.AddTransient(type);
            }
        }

        /// <summary>
        /// Registers the providers.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddProviders(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var registrableTypes = GetTypesThatInherit(typeof(IProvider), assembly);

            foreach (var type in registrableTypes)
            {
                var interfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault(x => typeof(IProvider).IsAssignableFrom(x));

                if (interfaceType == null)
                    continue;

                serviceCollection.AddTransient(interfaceType, type);
                serviceCollection.AddTransient(type);
            }
        }

        /// <summary>
        /// Registers the working tasks.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        public static void AddWorkingTasks(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddWorkingTasksOfType<IWorkTask>(typeof(WorkTask), assembly);
            serviceCollection.AddWorkingTasksOfType<IParallelWorkTask>(typeof(ParallelWorkTask), assembly);
        }

        private static void AddWorkingTasksOfType<T>(this IServiceCollection serviceCollection, Type implementationType, Assembly assembly)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddTransient(typeof(T), implementationType);
            var registrableTypes = GetTypesThatInherit(typeof(T), assembly);

            foreach (var type in registrableTypes)
            {
                var registrableInterfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault(x => typeof(T).IsAssignableFrom(x));

                if (registrableInterfaceType == null)
                    continue;

                serviceCollection.AddTransient(registrableInterfaceType, type);
                serviceCollection.AddTransient(type);
            }
        }

        /// <summary>
        /// Registers the types.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="type">The type.</param>
        /// <param name="assembly">Optional assembly to use as entry point. If no assembly is provided, the system will use the entry assembly. By default the system will use the entry assembly.</param>
        /// <exception cref="ArgumentNullException">serviceCollection</exception>
        private static void RegisterTypes(IServiceCollection serviceCollection, Type type, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var registrableTypes = GetTypesThatInherit(type, assembly);

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
            return (assembly ?? Assembly.GetEntryAssembly())
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Union(new[] { assembly ?? Assembly.GetEntryAssembly() })
                .SelectMany(x => x.DefinedTypes)
                .Where(x => type.IsAssignableFrom(x.AsType()) &&
                            !x.IsAbstract &&
                            !x.IsInterface &&
                            x.IsPublic)
                .ToList();
        }
    }
}