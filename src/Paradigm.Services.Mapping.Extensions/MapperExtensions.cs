using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Paradigm.Core.Mapping.Interfaces;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Mapping.Extensions
{
    public static class MapperExtensions
    {
        public static void AddMappings(this IMapper mapper, Assembly assembly = null, string mappingMehtod = "RegisterMapping")
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            var mappingTypes = GetMappingTypes(assembly, mappingMehtod);

            foreach(var mappingType in mappingTypes)
            {
                var registerMappingMethod = mappingType.GetMethod(mappingMehtod);
                registerMappingMethod.Invoke(null, new object[] { mapper });
            }

            mapper.Compile();
        }

        private static IEnumerable<TypeInfo> GetMappingTypes(Assembly assembly,  string mappingMehtod)
        {
            return (assembly ?? Assembly.GetEntryAssembly())
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Union(new[] { assembly ?? Assembly.GetEntryAssembly() })
                .SelectMany(x => x.DefinedTypes)
                .Where(x => typeof(DomainBase).IsAssignableFrom(x.AsType()) &&
                            !x.IsAbstract &&
                            !x.IsInterface &&
                            x.IsPublic &&
                            x.GetMethod(mappingMehtod) != null)
                .ToList();
        }
    }
}