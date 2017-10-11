/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using Paradigm.Services.Interfaces.Extensions;

namespace Paradigm.Services.Interfaces.Attributes
{
    /// <summary>
    /// Represents the base class for custom validation attributes.
    /// </summary>
    /// <remarks>
    /// Validation attributes are used by the framework to automatically validate domain entities, or at least
    /// any entity implementing a domain interface.
    /// </remarks>
    /// <seealso cref="System.Attribute" />
    public abstract class ValidationAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the field to be validated in a readable form.
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Gets the formatted error message that should be thrown if the validation is not accomplished.
        /// </summary>
        /// <remarks>
        /// If the message contains a content placeholder "{0}" will be replaced by the <see cref="FieldName"/>.
        /// </remarks>
        public virtual string FormattedErrorMessage => this.FormatErrorMessage(this.FieldName);

        /// <summary>
        /// Gets or sets the error message for this validation.
        /// </summary>
        /// <remarks>
        /// there is another way to provide a message for the validation, that comes from a resource manager instead of
        /// hardcoded string. If you want to localize your error message, see <see cref="ResourceType"/> and <see cref="ResourceName"/> properties.
        /// </remarks>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource manager containing the error message.
        /// </summary>
        /// <seealso cref="ResourceName"/>
        public Type ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource inside the resource manager.
        /// </summary>
        /// <seealso cref="ResourceType"/>
        public string ResourceName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceName">Name of the resource.</param>
        protected ValidationAttribute(string fieldName, Type resourceType = null, string resourceName = null)
        {
            this.FieldName = fieldName;
            this.ResourceType = resourceType;
            this.ResourceName = resourceName ?? this.GetType().Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="errorMessage">The error message.</param>
        protected ValidationAttribute(string fieldName, string errorMessage = null)
        {
            this.FieldName = fieldName;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttribute"/> class.
        /// </summary>
        protected ValidationAttribute()
        { 
        }

        /// <summary>
        /// Returns true if the value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsValid(object value)
        {
            return true;
        }
    }
}