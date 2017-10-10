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