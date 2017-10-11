using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Paradigm.Services.Interfaces.Attributes;

namespace Paradigm.Services.Interfaces.Validations
{
    /// <summary>
    /// Provides the means to validate domain interfaces.
    /// </summary>
    /// <seealso cref="Paradigm.Services.Interfaces.Validations.IDomainInterfaceValidator" />
    public class DomainInterfaceValidator : IDomainInterfaceValidator
    {
        /// <summary>
        /// Validates the specified domain interface.
        /// </summary>
        /// <param name="domainInterface">The domain interface.</param>
        /// <param name="ignoreInterfaceTypes">A list of classes to ignore.</param>
        /// <param name="ignoreProperties">A list of properties to ignore.</param>
        /// <returns>
        /// A list of validation messages.
        /// </returns>
        public IReadOnlyCollection<IPropertyValidation> Validate(IDomainInterface domainInterface, IEnumerable<string> ignoreInterfaceTypes = null, IEnumerable<string> ignoreProperties = null)
        {
            var validations = new List<IPropertyValidation>();
            var typeInfo = domainInterface.GetType().GetTypeInfo();
            var properties = GetProperties(typeInfo, ignoreProperties);
            var hierarchy = GetHierarchy(typeInfo, ignoreInterfaceTypes);

            foreach (var property in properties)
            {
                var propertyValidations = new PropertyValidation(property);
                var validators = hierarchy
                    .SelectMany(x => x.DeclaredProperties.Where(p => p.Name == property.Name))
                    .SelectMany(x => x.GetCustomAttributes())
                    .Select(x => x as ValidationAttribute)
                    .Where(x => x != null)
                    .ToList();

                foreach (var validator in validators)
                {
                    if (validator.IsValid(property.GetValue(domainInterface)))
                        continue;

                    propertyValidations.Add(new PropertyValidationError(validator.FormattedErrorMessage));
                }

                if (propertyValidations.Errors.Any())
                    validations.Add(propertyValidations);
            }

            return validations;
        }

        /// <summary>
        /// Gets the interface hierarchy.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="ignoreClasses">The types that can be ignored.</param>
        /// <returns>List of types of interfaces that the <see cref="typeInfo"/> extends.</returns>
        private static List<TypeInfo> GetHierarchy(TypeInfo typeInfo, IEnumerable<string> ignoreClasses = null)
        {
            var typesInfo = new List<TypeInfo>();
            var interfaces = typeInfo.ImplementedInterfaces;
            var ignore = ignoreClasses as IList<string> ?? ignoreClasses?.ToList();

            if (ignore != null)
                interfaces = interfaces.Where(x => !ignore.Contains(x.Name)).ToList();

            typesInfo.AddRange(interfaces.Select(x => x.GetTypeInfo()));

            while (typeInfo != null)
            {            
                if (!(ignore?.Contains(typeInfo.Name) ?? false))
                    typesInfo.Add(typeInfo);

                typeInfo = typeInfo.BaseType?.GetTypeInfo();
            }

            return typesInfo;
        }

        /// <summary>
        /// Gets a list of all the properties than can be validated.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="ignoreProperties">The ignore properties.</param>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> GetProperties(TypeInfo typeInfo, IEnumerable<string> ignoreProperties = null)
        {
            var properties = typeInfo.DeclaredProperties;

            if (ignoreProperties != null)
                properties = properties.Where(x => !ignoreProperties.Contains(x.Name));

            return properties.ToList();
        }
    }
}