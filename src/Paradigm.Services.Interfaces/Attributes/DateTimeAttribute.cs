using System;

namespace Paradigm.Services.Interfaces.Attributes
{
    /// <summary>
    /// Represents a DateTime value validation.
    /// </summary>
    /// <remarks>
    /// Validates that a date is between a valid minimum and maximum values.
    /// </remarks>
    /// <seealso cref="Paradigm.Services.Interfaces.Attributes.ValidationAttribute" />
    public class DateTimeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the minimum valid value.
        /// </summary>
        public DateTime MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum valid value.
        /// </summary>
        public DateTime MaxValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceName">Name of the resource.</param>
        public DateTimeAttribute(string fieldName, string minValue, string maxValue, Type resourceType = null, string resourceName = null) 
            : base(fieldName, resourceType, resourceName)
        {
            this.MinValue = DateTime.Parse(minValue);
            this.MaxValue = DateTime.Parse(maxValue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="errorMessage">The error message.</param>
        public DateTimeAttribute(string fieldName, string minValue, string maxValue, string errorMessage = null)
            : base(fieldName, errorMessage)
        {
            this.MinValue = DateTime.Parse(minValue);
            this.MaxValue = DateTime.Parse(maxValue);
        }

        /// <summary>
        /// Returns true if the value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        /// <c>true</c> if the specified value is between the valid minimum and maximum range; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {    
            var date = value as DateTime?;
         
            if (date == null)
                return true;

            return date.Value >= this.MinValue && 
                   date.Value <= this.MaxValue;
        }
    }
}