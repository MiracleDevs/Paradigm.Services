using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Domain;
using Paradigm.Services.Providers;
using Paradigm.Services.Repositories;
using Paradigm.Services.Repositories.UOW;
using Paradigm.Services.WorkingTasks;

namespace Paradigm.Services.Mvc.Extensions
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
        public static void UseUnitOfWork(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Registers the domain classes.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point.</param>
        public static void UseDomain(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            RegisterTypes(serviceCollection, typeof(DomainBase), assembly);
        }

        /// <summary>
        /// Registers the repositories.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="assembly">Optional assembly to use as entry point.</param>
        public static void UseRepositories(this IServiceCollection serviceCollection, Assembly assembly = null)
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
        /// <param name="assembly">Optional assembly to use as entry point.</param>
        public static void UseProviders(this IServiceCollection serviceCollection, Assembly assembly = null)
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
        /// <param name="assembly">Optional assembly to use as entry point.</param>
        public static void UseWorkingTasks(this IServiceCollection serviceCollection, Assembly assembly = null)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var registrableTypes = GetTypesThatInherit(typeof(IWorkTask), assembly);

            foreach (var type in registrableTypes)
            {
                var interfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault(x => typeof(IWorkTask).IsAssignableFrom(x));

                if (interfaceType == null)
                    continue;

                serviceCollection.AddTransient(interfaceType, type);
                serviceCollection.AddTransient(type);
            }
        }

        /// <summary>
        /// Registers the types.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="type">The type.</param>
        /// <param name="assembly">Optional assembly to use as entry point.</param>
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
        /// <param name="assembly">Optional assembly to use as entry point.</param>
        /// <returns></returns>
        private static List<TypeInfo> GetTypesThatInherit(Type type, Assembly assembly = null)
        {
            return (assembly ?? Assembly.GetCallingAssembly())
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(x => type.IsAssignableFrom(x.AsType()) &&
                            !x.IsAbstract &&
                            !x.IsInterface &&
                            x.IsPublic)
                .ToList();
        }
    }
}