using System;
using System.Resources;

namespace Paradigm.Services.Exceptions
{
    /// <summary>
    /// Provides the base functionality for objects in charge to match certain exceptions, and return a custom message for it.
    /// </summary>
    /// <seealso cref="IExceptionMatcher"/>
    public abstract class ExceptionMatcherBase : IExceptionMatcher
    {
        /// <summary>
        /// Indicates if the exception provided is a match of the current matcher, and the message can be replaced.
        /// </summary>
        /// <param name="ex">The ex to check if its a match.</param>
        /// <returns>
        /// True if the exception is a match, false otherwise.
        /// </returns>
        public abstract bool Match(Exception ex);

        /// <summary>
        /// Gets a new message string for the exception that was previously matched with the <see cref="Match(Exception)" /> method.
        /// </summary>
        /// <param name="resourceManager">The resource manager from which the message will be taken.</param>
        /// <param name="ex">The previously matched exception.</param>
        /// <returns>
        /// A new exception message to replace the original exception message.
        /// </returns>
        public abstract string GetNewMessage(ResourceManager resourceManager, Exception ex);
    }
}