/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

namespace Paradigm.Services.Interfaces.Validations
{
    /// <summary>
    /// Provides an interface for a property validation error.
    /// </summary>
    public interface IPropertyValidationError
    {
        /// <summary>
        /// Gets the validation message.
        /// </summary>
        string Message { get; }
    }
}