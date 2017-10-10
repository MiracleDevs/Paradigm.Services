using System.Collections.Generic;
using System.Reflection;

namespace Paradigm.Services.Interfaces.Validations
{
    /// <summary>
    /// Provides the interface for a property validation object.
    /// </summary>
    public interface IPropertyValidation
    {
        /// <summary>
        /// Gets the property being validated.
        /// </summary>
        PropertyInfo Property { get; }

        /// <summary>
        /// Gets a readonly collection of validation errors.
        /// </summary>
        IReadOnlyCollection<IPropertyValidationError> Errors { get; }
    }
}