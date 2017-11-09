using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.CommandLineUtils;
using Paradigm.Core.Extensions;

namespace Paradigm.Services.CLI
{
    public class ArgumentParser
    {
        #region Nested Types

        /// <summary>
        /// Provides a container for argument options and their related attributes.
        /// </summary>
        internal class Option
        {
            /// <summary>
            /// Gets the argument option attribute.
            /// </summary>
            /// <value>
            /// The argument option attribute.
            /// </value>
            public ArgumentOptionAttribute ArgumentOptionAttribute { get; }

            /// <summary>
            /// Gets the command option.
            /// </summary>
            /// <value>
            /// The command option.
            /// </value>
            public CommandOption CommandOption { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Option"/> class.
            /// </summary>
            /// <param name="argumentOptionAttribute">The argument option attribute.</param>
            /// <param name="commandOption">The command option.</param>
            /// <exception cref="ArgumentNullException">
            /// argumentOptionAttribute
            /// or
            /// commandOption
            /// </exception>
            public Option(ArgumentOptionAttribute argumentOptionAttribute, CommandOption commandOption)
            {
                this.ArgumentOptionAttribute = argumentOptionAttribute ?? throw new ArgumentNullException(nameof(argumentOptionAttribute));
                this.CommandOption = commandOption ?? throw new ArgumentNullException(nameof(commandOption));
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets the console line arguments.
        /// </summary>
        /// <value>
        /// The console line arguments.
        /// </value>
        public string[] LineArguments { get; private set; }

        /// <summary>
        /// Gets or sets the type of the arguments.
        /// </summary>
        /// <value>
        /// The type of the arguments.
        /// </value>
        private Type ArgumentsType { get; set; }

        /// <summary>
        /// Gets or sets the command line application parser.
        /// </summary>
        /// <value>
        /// The command line application parser.
        /// </value>
        private CommandLineApplication CommandLineApplication { get; set; }

        /// <summary>
        /// Gets or sets the options and their properties.
        /// </summary>
        /// <value>
        /// The options and their properties.
        /// </value>
        private Dictionary<PropertyInfo, Option> Options { get; set; }

        /// <summary>
        /// Gets the short representation of the version.
        /// </summary>
        /// <value>
        /// The short representation of the version.
        /// </value>
        public string ShortVersion { get; private set; }

        /// <summary>
        /// Gets the long representation of the version.
        /// </summary>
        /// <value>
        /// The long representation of the version.
        /// </value>
        public string LongVersion { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses the console line arguments.
        /// </summary>
        /// <typeparam name="T">A type decorated with the options</typeparam>
        /// <param name="args">The console line arguments arguments.</param>
        /// <returns></returns>
        public void ParseArguments<T>(string[] args) where T : class
        {
            this.LineArguments = args;
            this.CommandLineApplication = new CommandLineApplication(false);
            this.Options = new Dictionary<PropertyInfo, Option>();
            this.ArgumentsType = typeof(T);
            var properties = this.ArgumentsType.GetProperties();
            var helpOptionAttribute = this.ArgumentsType.GetCustomAttribute<HelpOptionAttribute>();
            var versionOptionAttribute = this.ArgumentsType.GetCustomAttribute<VersionOptionAttribute>();

            if (helpOptionAttribute != null)
                this.CommandLineApplication.HelpOption(helpOptionAttribute.Template);

            if (versionOptionAttribute != null)
                this.CommandLineApplication.VersionOption(versionOptionAttribute.Template, () => this.ShortVersion, () => this.LongVersion);

            foreach (var property in properties)
            {
                var argumentOptionAttribute = property.GetCustomAttribute<ArgumentOptionAttribute>();

                if (argumentOptionAttribute == null)
                    continue;

                this.Options.Add(property, new Option(argumentOptionAttribute, this.CommandLineApplication.Option(argumentOptionAttribute.GetTemplate(property.Name), argumentOptionAttribute.Description, argumentOptionAttribute.Type)));
            }
        }

        /// <summary>
        /// Sets the console application version.
        /// </summary>
        /// <param name="shortVersion">The short version.</param>
        /// <param name="longVersion">The long version.</param>
        /// <returns></returns>
        public void SetVersion(string shortVersion, string longVersion)
        {
            this.ShortVersion = shortVersion;
            this.LongVersion = longVersion;
        }


        public int Run(Action<Type, object> onRun = null, Action<Exception> onError = null)
        {
            this.CommandLineApplication.OnExecute(() =>
            {
                try
                {
                    onRun?.Invoke(this.ArgumentsType, this.GetTypedArguments());
                    return 0;
                }
                catch (Exception ex)
                {
                    onError?.Invoke(ex);
                    return -1;
                }
            });

            return this.CommandLineApplication.Execute(this.LineArguments);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Configures the argument object with the command line arguments.
        /// </summary>
        /// <exception cref="System.Exception">Couldn't create an instance of argument object provided.</exception>
        private object GetTypedArguments()
        {
            if (this.CommandLineApplication == null || this.Options == null)
                return null;

            var arguments = Activator.CreateInstance(this.ArgumentsType);
            var exceptions = new List<Exception>();

            if (arguments == null)
                throw new Exception("Couldn't create an instance of argument object provided.");

            foreach (var property in this.Options.Keys)
            {
                try
                {
                    var option = this.Options[property];

                    switch (option.CommandOption.OptionType)
                    {
                        case CommandOptionType.NoValue:
                            continue;

                        case CommandOptionType.SingleValue:
                            var singleValue = option.CommandOption.Value();
                            property.SetValue(arguments, GetArgumentValue(option.CommandOption.Template, property.PropertyType, singleValue, option.ArgumentOptionAttribute.DefaultValue));
                            break;

                        default:
                            var list = Activator.CreateInstance(property.PropertyType) as IList;
                            var listItemType = property.PropertyType.GetGenericArguments().FirstOrDefault();

                            if (list == null)
                                throw new Exception($"Couldn't create the argument list for argument '{property.Name}'.");

                            foreach (var value in option.CommandOption.Values)
                            {
                                list.Add(GetArgumentValue(option.CommandOption.Template, listItemType, value, option.ArgumentOptionAttribute.DefaultValue));
                            }

                            property.SetValue(arguments, list);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
                throw new AggregateException(exceptions);

            return arguments;
        }

        /// <summary>
        /// Gets the argument value.
        /// </summary>
        /// <param name="template">The argument template.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A parsed value.</returns>
        /// <exception cref="Exception"></exception>
        private static object GetArgumentValue(string template, Type type, string value, object defaultValue)
        {
            var allowNulls = type.IsInterface || type.IsClass || Nullable.GetUnderlyingType(type) != null;
            var innerType = Nullable.GetUnderlyingType(type);

            if (value == null)
            {
                return allowNulls
                    ? (innerType != null && defaultValue != null 
                        ? Convert.ChangeType(defaultValue, innerType) 
                        : defaultValue)
                    : throw new Exception($"Parameter '{template}' is mandatory.");
            }

            if (innerType != null && innerType.IsEnum)
            {
                return Enum.Parse(innerType, value);
            }

            if (innerType != null && innerType == typeof(TimeSpan))
            {
                return TimeSpan.Parse(value);
            }

            if (innerType != null && innerType == typeof(DateTime))
            {
                return DateTime.Parse(value);
            }

            if (innerType != null && innerType == typeof(DateTimeOffset))
            {
                return DateTimeOffset.Parse(value);
            }

            if (innerType != null && innerType == typeof(Guid))
            {
                return Guid.Parse(value);
            }

            var converter = TypeDescriptor.GetConverter(type);

            if (converter.CanConvertFrom(typeof(string)))
                return converter.ConvertFrom(value);

            if (defaultValue == null && !allowNulls)
                throw new Exception($"Can not parse the value for '{template}' and the argument does not have a default value.");

            return defaultValue;
        }

        #endregion
    }
}