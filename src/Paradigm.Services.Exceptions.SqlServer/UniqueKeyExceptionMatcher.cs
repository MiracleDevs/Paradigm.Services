using System;
using System.Data.SqlClient;
using System.Resources;
using System.Text.RegularExpressions;

namespace Paradigm.Services.Exceptions.SqlServer
{
    /// <summary>
    /// Matches SqlServer Unique key exceptions and provide a more readable error message.
    /// </summary>
    /// <seealso cref="Paradigm.Services.Exceptions.ExceptionMatcherBase" />
    public class UniqueKeyExceptionMatcher : ExceptionMatcherBase
    {
        /// <summary>
        /// A regex expression to parse the SqlServer unique key exception message.
        /// </summary>
        private const string MessageRegex = @"Violation of (.*) constraint '(.*)'. Cannot insert duplicate key in object '(.*)'. The duplicate key value is \((.*)\).";

        /// <summary>
        /// The default message key to search inside a resource manager.
        /// </summary>
        private const string DefaultMessageKey = "UniqueKeyDefaultMessage";

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
            return sqlEx != null && (sqlEx.Number == 2601 || sqlEx.Number == 2627); //we used 2627 which includes primary keys, because we never send ids.
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
            var table = match.Groups[3].Value.Replace("dbo.", "");

            var message = resourceManager.GetString(key) ?? resourceManager.GetString(DefaultMessageKey);
            return message != null ? string.Format(message, table) : null;
        }
    }
}