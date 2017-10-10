namespace Paradigm.Services.Interfaces.Validations
{
    /// <summary>
    /// Represents a property validation error.
    /// </summary>
    /// <seealso cref="Paradigm.Services.Interfaces.Validations.IPropertyValidationError" />
    public class PropertyValidationError : IPropertyValidationError
    {
        /// <summary>
        /// Gets the validation message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValidationError"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        internal PropertyValidationError(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Message;
        }
    }
}