using System;

namespace Paradigm.Services.Interfaces.Attributes
{
    /// <summary>
    /// Represents an integer non zero validation.
    /// </summary>
    /// <remarks>
    /// Validates that a given integer value is not 0.
    /// Is useful to validate properties that match with table Ids or foreign keys.
    /// </remarks>
    /// <seealso cref="Paradigm.Services.Interfaces.Attributes.ValidationAttribute" />
    public class NonZeroAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonZeroAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceName">Name of the resource.</param>
        public NonZeroAttribute(string fieldName, Type resourceType = null, string resourceName = null)
            : base(fieldName, resourceType, resourceName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonZeroAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="errorMessage">The error message.</param>
        public NonZeroAttribute(string fieldName, string errorMessage = null)
            : base(fieldName, errorMessage)
        {
        }

        /// <summary>
        /// Returns true if value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        /// <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value is int intValue)
                return intValue > 0;

            return true;
        }
    }
}