using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Paradigm.Services.CLI
{
    /// <summary>
    /// Represents a normal console argument option.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class ArgumentOptionAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Gets the short argument caption.
        /// </summary>
        /// <value>
        /// The short argument caption.
        /// </value>
        public string ShortArgument { get; }

        /// <summary>
        /// Gets the large argument caption.
        /// </summary>
        /// <value>
        /// The large argument caption.
        /// </value>
        public string LargeArgument { get; }

        /// <summary>
        /// Gets the argument description.
        /// </summary>
        /// <value>
        /// The argument description.
        /// </value>
        public string Description { get; }

        /// <summary>
        /// Gets the argument type.
        /// </summary>
        /// <value>
        /// The argument type.
        /// </value>
        public CommandOptionType Type { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentOptionAttribute"/> class.
        /// </summary>
        /// <param name="shortCommand">The short command.</param>
        /// <param name="largeCommand">The large command.</param>
        /// <param name="description">The description.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="System.ArgumentNullException">
        /// shortCommand
        /// or
        /// largeCommand
        /// or
        /// description
        /// </exception>
        public ArgumentOptionAttribute(string shortCommand, string largeCommand, string description, CommandOptionType type)
        {
            this.ShortArgument = shortCommand ?? throw new ArgumentNullException(nameof(shortCommand));
            this.LargeArgument = largeCommand ?? throw new ArgumentNullException(nameof(largeCommand));
            this.Description = description ?? throw new ArgumentNullException(nameof(description));
            this.Type = type;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        internal string GetTemplate(string propertyName)
        {
            return this.Type == CommandOptionType.NoValue
                ? $"{this.ShortArgument} | {this.LargeArgument}"
                : $"{this.ShortArgument} | {this.LargeArgument} <{propertyName}>";
        }

        #endregion
    }
}