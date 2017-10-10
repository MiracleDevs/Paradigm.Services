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
