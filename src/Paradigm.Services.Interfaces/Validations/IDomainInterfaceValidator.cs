using System.Collections.Generic;

namespace Paradigm.Services.Interfaces.Validations
{
    /// <summary>
    /// Provides the interface for a interface validator object.
    /// </summary>
    public interface IDomainInterfaceValidator
    {
        /// <summary>
        /// Validates the specified domain interface.
        /// </summary>
        /// <param name="domainInterface">The domain interface.</param>
        /// <param name="ignoreInterfaceTypes">A list of classes to ignore.</param>
        /// <param name="ignoreProperties">A list of properties to ignore.</param>
        /// <returns>A list of validation messages.</returns>
        IReadOnlyCollection<IPropertyValidation> Validate(IDomainInterface domainInterface, IEnumerable<string> ignoreInterfaceTypes = null, IEnumerable<string> ignoreProperties = null);
    }
}