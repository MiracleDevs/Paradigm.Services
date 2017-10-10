using System;

namespace Paradigm.Services.Exceptions
{
    /// <summary>
    /// Provides the interface for exception handler objects that can match and replace existing exception messages by
    /// more readable messages.
    /// </summary>
    /// <remarks>
    /// When working with certain type of exceptions, like database exceptions, sometimes the messages provided are not
    /// meaningful for the end user. The exception handler  with the help of various <see cref="IExceptionMatcher"/> provides a way to catch,
    /// match and replace the exception messages for more readable versions of it.
    /// </remarks>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Adds a new <see cref="IExceptionMatcher"/> to the exception matcher collection.
        /// </summary>
        /// <param name="matcher">The matcher to be added.</param>
        void AddMatcher(IExceptionMatcher matcher);

        /// <summary>
        /// Handles an exception being throw.
        /// </summary>
        /// <param name="ex">The exception to be handled.</param>
        /// <returns>Returns the same exception, or a new one with more readable message and the original exception as an inner exception if it was matched.</returns>
        Exception Handle(Exception ex);
    }
}