using System;

namespace Paradigm.Services.CLI
{
    /// <summary>
    /// Represents a console help argument.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class HelpOptionAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Gets the argument caption list.
        /// </summary>
        /// <example>
        /// new string [] { "-h", "--help", "?", "-?" } 
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
        /// Initializes a new instance of the <see cref="HelpOptionAttribute"/> class.
        /// </summary>
        /// <param name="arguments">The argument caption list.</param>
        /// <exception cref="System.ArgumentNullException">arguments</exception>
        public HelpOptionAttribute(string[] arguments)
        {
            this.Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        #endregion
    }
}