using System;

namespace Paradigm.Services.Interfaces.Attributes
{
    /// <summary>
    /// Represents a requirement validation.
    /// </summary>
    /// <remarks>
    /// This validation checks if a value that is mandatory is null.
    /// </remarks>
    /// <seealso cref="Paradigm.Services.Interfaces.Attributes.ValidationAttribute" />
    public class RequiredAttribute: ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceName">Name of the resource.</param>
        public RequiredAttribute(string fieldName, Type resourceType = null, string resourceName = null) 
            : base(fieldName, resourceType, resourceName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="errorMessage">The error message.</param>
        public RequiredAttribute(string fieldName,  string errorMessage = null)
            : base(fieldName, errorMessage)
        {
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        /// <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}