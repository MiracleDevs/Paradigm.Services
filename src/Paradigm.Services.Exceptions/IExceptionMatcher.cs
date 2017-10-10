using System;
using System.Resources;

namespace Paradigm.Services.Exceptions
{
    /// <summary>
    /// Provides an interface for objects in charge to match certain exceptions, and return a custom message for it.
    /// </summary>
    /// <remarks>
    /// When working with certain type of exceptions, like database exceptions, sometimes the messages provided are not
    /// meaningful for the end user. These matchers, with the <see cref="IExceptionHandler"/> provides a way to catch,
    /// match and replace the exception messages for more readable versions of it.
    /// </remarks>
    public interface IExceptionMatcher
    {
        /// <summary>
        /// Indicates if the exception provided is a match of the current matcher, and the message can be replaced.
        /// </summary>
        /// <param name="ex">The ex to check if its a match.</param>
        /// <returns>True if the exception is a match, false otherwise.</returns>
        bool Match(Exception ex);

        /// <summary>
        /// Gets a new message string for the exception that was previously matched with the <see cref="Match(Exception)"/> method.
        /// </summary>
        /// <param name="resourceManager">The resource manager from which the message will be taken.</param>
        /// <param name="ex">The previously matched exception.</param>
        /// <returns>A new exception message to replace the original exception message.</returns>
        string GetNewMessage(ResourceManager resourceManager, Exception ex);
    }
}