using System;
using System.Data.SqlClient;
using System.Resources;
using System.Text.RegularExpressions;

namespace Paradigm.Services.Exceptions.SqlServer
{
    /// <summary>
    /// Matches SqlServer Foreign key exceptions and provide a more readable error message.
    /// </summary>
    /// <seealso cref="Paradigm.Services.Exceptions.ExceptionMatcherBase" />
    public class ForeignKeyExceptionMatcher : ExceptionMatcherBase
    {
        /// <summary>
        /// A regex expression to parse the SqlServer foreign key exception message.
        /// </summary>
        private const string MessageRegex = "The (.*) statement conflicted with the (.*) constraint \"(.*)\". The conflict occurred in database \"(.*)\", table \"(.*)\"";

        /// <summary>
        /// The default message key to search inside a resource manager.
        /// </summary>
        private const string DefaultMessageKey = "ForeignKeyDefaultMessage";

        /// <summary>
        /// Indicates if the exception provided is a match of the current matcher, and the message can be replaced.
        /// </summary>
        /// <param name="ex">The ex to check if its a match.</param>
        /// <returns>
        /// True if the exception is a match, false otherwise.
        /// </returns>
        public override bool Match(Exception ex)
        {
            var sqlEx = ex as SqlException;
            return sqlEx != null && sqlEx.Number == 547; 
        }

        /// <summary>
        /// Gets a new message string for the exception that was previously matched with the <see cref="M:Paradigm.Services.Exceptions.ExceptionMatcherBase.Match(System.Exception)" /> method.
        /// </summary>
        /// <param name="resourceManager">The resource manager from which the message will be taken.</param>
        /// <param name="ex">The previously matched exception.</param>
        /// <returns>
        /// A new exception message to replace the original exception message.
        /// </returns>
        public override string GetNewMessage(ResourceManager resourceManager, Exception ex)
        {
            var match = Regex.Match(ex.Message, MessageRegex);
            var key = match.Groups[2].Value;
   
            var message = resourceManager.GetString(key) ?? resourceManager.GetString(DefaultMessageKey);
            return message ?? string.Empty;
        }
    }
}