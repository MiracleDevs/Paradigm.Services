using System;

namespace Paradigm.Services.Exceptions
{
    /// <summary>
    /// Represents errors that occurs during an authentication processes.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AuthenticationException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AuthenticationException(string message): base(message)
        {

        }
    }
}