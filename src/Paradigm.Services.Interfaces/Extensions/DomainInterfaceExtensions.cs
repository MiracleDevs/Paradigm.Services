using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paradigm.Services.Interfaces;
using Paradigm.Services.Interfaces.Validations;

namespace Paradigm.Services.Interfaces.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IDomainInterface"/>.
    /// </summary>
    public static class DomainInterfaceExtensions
    {
        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ignoreClasses">A list of classes that needs to be ignored when validating.</param>
        /// <param name="ignoreProperties">A list of properties that needs to be ignored when validating.</param>
        /// <returns>A list of validation errors.</returns>
        public static IReadOnlyCollection<IPropertyValidation> Validate(this IDomainInterface entity, IEnumerable<string> ignoreClasses = null, IEnumerable<string> ignoreProperties = null)
        {
            return new DomainInterfaceValidator().Validate(entity, ignoreClasses, ignoreProperties);
        }

        /// <summary>
        /// Validates the specified entity and throws an exception if the validation fails.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="ignoreClasses">A list of classes that needs to be ignored when validating.</param>
        /// <param name="ignoreProperties">A list of properties that needs to be ignored when validating.</param>
        public static void ValidateAndThrow(this IDomainInterface entity, IEnumerable<string> ignoreClasses = null, IEnumerable<string> ignoreProperties = null)
        {
            var validators = entity.Validate(ignoreClasses, ignoreProperties);

            if (!validators.Any())
                return;

            var builder = new StringBuilder();
            builder.AppendLine("Validation errors:");

            foreach (var validator in validators)
            {
                builder.AppendLine(string.Join(Environment.NewLine, validator.Errors.Select(x => $" - {x.Message}")));
            }

            throw new Exception(builder.ToString());
        }
    }
}