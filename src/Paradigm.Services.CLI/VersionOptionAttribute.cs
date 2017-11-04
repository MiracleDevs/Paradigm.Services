using System;

namespace Paradigm.Services.CLI
{
    /// <summary>
    /// Represents a console version argument.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionOptionAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Gets the argument caption list.
        /// </summary>
        /// <example>
        /// new string [] { "-v", "--version" } 
        /// </example>
        /// <value>
        /// The argument caption list.
        /// </value>
        public string[] Arguments { get; }

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        internal string Template => string.Join(" | ", this.Arguments);

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionOptionAttribute"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="System.ArgumentNullException">arguments</exception>
        public VersionOptionAttribute(string[] arguments)
        {
            this.Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        #endregion
    }
}