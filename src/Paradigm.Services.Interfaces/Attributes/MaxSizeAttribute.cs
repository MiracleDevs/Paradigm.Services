/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections;
using Paradigm.Services.Interfaces.Extensions;

namespace Paradigm.Services.Interfaces.Attributes
{
    /// <summary>
    /// Represents a array or collection size validation.
    /// </summary>
    /// <remarks>
    /// This validator can validate either arrays or collection. 
    /// Can be used validate string length, byte array length, etc.
    /// </remarks>
    /// <seealso cref="Paradigm.Services.Interfaces.Attributes.ValidationAttribute" />
    public class MaxSizeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        public long MaxLength { get; }

        /// <summary>
        /// Gets the formatted error message that should be thrown if the validation is not accomplished.
        /// </summary>
        /// <remarks>
        /// If the message contains content placeholders, then:
        /// {0}: will be replaced by the <see cref="P:Paradigm.Services.Interfaces.Attributes.ValidationAttribute.FieldName" />.
        /// {1}: will be replaced by <see cref="MaxLength"/>.
        /// </remarks>
        public override string FormattedErrorMessage => this.FormatErrorMessage(this.FieldName, this.MaxLength);

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxSizeAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceName">Name of the resource.</param>
        public MaxSizeAttribute(string fieldName, long maxLength, Type resourceType = null, string resourceName = null) 
            : base(fieldName, resourceType, resourceName)
        {
            this.MaxLength = maxLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxSizeAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="errorMessage">The error message.</param>
        public MaxSizeAttribute(string fieldName, long maxLength, string errorMessage = null)
            : base(fieldName, errorMessage)
        {
            this.MaxLength = maxLength;
        }

        /// <summary>
        /// Returns true if the value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        /// <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value is Array arrayValue)
                return arrayValue.Length <= this.MaxLength;

            if (value is ICollection collectionValue)
                return collectionValue.Count <= this.MaxLength;

            return true;
        }
    }
}