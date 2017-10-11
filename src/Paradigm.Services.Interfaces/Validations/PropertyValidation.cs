/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Paradigm.Services.Interfaces.Validations
{
    /// <summary>
    /// Represents a property validation.
    /// </summary>
    /// <seealso cref="Paradigm.Services.Interfaces.Validations.IPropertyValidation" />
    public class PropertyValidation : IPropertyValidation
    {
        /// <summary>
        /// Gets the property being validated.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Gets a readonly collection of validation errors.
        /// </summary>
        public IReadOnlyCollection<IPropertyValidationError> Errors => this.ErrorList;

        /// <summary>
        /// Gets the error list.
        /// </summary>
        private List<IPropertyValidationError> ErrorList { get;  }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValidation"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public PropertyValidation(PropertyInfo property)
        {
            Property = property;
            ErrorList = new List<IPropertyValidationError>();
        }

        /// <summary>
        /// Adds a new validation error.
        /// </summary>
        /// <param name="error">The error.</param>
        internal void Add(IPropertyValidationError error)
        {
            this.ErrorList.Add(error);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.Property.Name} has the following errors:{Environment.NewLine}{string.Join($"{Environment.NewLine} -", this.ErrorList)}";
        }
    }
}