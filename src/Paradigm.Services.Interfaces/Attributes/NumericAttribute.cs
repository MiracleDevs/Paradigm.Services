using System;
using System.Linq;
using Paradigm.Services.Interfaces.Extensions;

namespace Paradigm.Services.Interfaces.Attributes
{
    /// <summary>
    /// Represents a numeric structure validation.
    /// </summary>
    /// <remarks>
    /// Will validate if the number is being expressed with the propery scale and precision.
    /// </remarks>
    /// <seealso cref="Paradigm.Services.Interfaces.Attributes.ValidationAttribute" />
    public class NumericAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the numeric scale.
        /// </summary>
        public int Scale { get; }

        /// <summary>
        /// Gets the numeric precision.
        /// </summary>
        public int Precision { get; }

        /// <summary>
        /// Gets the formatted error message that should be thrown if the validation is not accomplished.
        /// </summary>
        /// <remarks>
        /// If the message contains content placeholders, then:
        /// {0}: will be replaced by the <see cref="P:Paradigm.Services.Interfaces.Attributes.ValidationAttribute.FieldName" />.
        /// {1}: will be replaced by the <see cref="Scale"/>.
        /// {2}: will be replaced by the <see cref="Precision"/>
        /// </remarks>
        public override string FormattedErrorMessage => this.FormatErrorMessage(this.FieldName, this.Scale, this.Precision);

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="precision">The precision.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceName">Name of the resource.</param>
        public NumericAttribute(string fieldName, int precision, int scale, Type resourceType = null, string resourceName = null)
            : base(fieldName, resourceType, resourceName)
        {
            this.Scale = scale;
            this.Precision = precision;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="precision">The precision.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="errorMessage">The error message.</param>
        public NumericAttribute(string fieldName, int precision, int scale, string errorMessage = null)
            : base(fieldName, errorMessage)
        {
            this.Scale = scale;
            this.Precision = precision;
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
            // the objective of this validation is not to validate if 
            // if null is correct or not, just to validate precision and scale.
            if (value == null)
                return true;

            // some databases like mysql treat bool as tinyints, and so
            // the boolean inherits the numeric validaitons, so we exit then.
            if (value is bool)
                return true;

            var stringValue = value.ToString();

            if (this.Scale == 0)
                return stringValue.Where(char.IsDigit).Count() <= this.Precision;

            var decimalParts = stringValue.Split('.');

            ////////////////////////////////////////////////////
            // Remove trailing zeros from decimal
            ////////////////////////////////////////////////////
            if (decimalParts.Length > 1)
            {
                var decimalPart = decimalParts[1];
                var length = decimalPart.Length;

                for (var i = length - 1; i >= 0; i--, length--)
                {
                    var c = decimalPart[i];

                    if (c != 'M' && c != 'm' && c != '0')
                        break;
                }

                decimalParts[1] = decimalPart.Substring(0, length);
            }

            return decimalParts[0].Length <= this.Precision - this.Scale &&
                   (decimalParts.Length == 1 || decimalParts[1].Length <= this.Scale);
        }
    }
}
