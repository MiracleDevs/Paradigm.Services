/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

namespace Paradigm.Services.Interfaces
{
    /// <summary>
    /// Provides a common base inteface for other domain interfaces.
    /// </summary>
    public interface IDomainInterface
    {
        /// <summary>
        /// Determines whether this instance is new.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </returns>
        bool IsNew();
    }
}
