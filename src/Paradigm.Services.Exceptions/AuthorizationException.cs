using System;

namespace Paradigm.Services.Exceptions
{
    /// <summary>
    /// Represents errors that occurs during an authorization processes.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AuthorizationException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AuthorizationException(string message): base(message)
        {
        }
    }
}