using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.Services.CLI
{
    /// <summary>
    /// Provides convenience methods to create a console application.
    /// </summary>
    public class ConsoleHost
    {
        #region Properties

        /// <summary>
        /// Gets the console line arguments.
        /// </summary>
        /// <value>
        /// The console line arguments.
        /// </value>
        public string[] LineArguments { get; private set; }

        /// <summary>
        /// Gets the startup program.
        /// </summary>
        /// <value>
        /// The startup program.
        /// </value>
        public IStartup Startup { get; private set; }

        /// <summary>
        /// Gets the service collection.
        /// </summary>
        /// <value>
        /// The service collection.
        /// </value>
        public IServiceCollection ServiceCollection { get; private set; }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Gets the configuration root.
        /// </summary>
        /// <value>
        /// The configuration root.
        /// </value>
        public IConfigurationRoot ConfigurationRoot { get; private set; }

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
        private Dictionary<PropertyInfo, CommandOption> Options { get; set; }

        /// <summary>
        /// Gets or sets the type of the arguments.
        /// </summary>
        /// <value>
        /// The type of the arguments.
        /// </value>
        private Type ArgumentsType { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="ConsoleHost"/> class from being created.
        /// </summary>
        private ConsoleHost()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public static ConsoleHost Create()
        {
            return new ConsoleHost();
        }

        /// <summary>
        /// Parses the console line arguments.
        /// </summary>
        /// <typeparam name="T">A type decorated with the options</typeparam>
        /// <param name="args">The console line arguments arguments.</param>
        /// <returns></returns>
        public ConsoleHost ParseArguments<T>(string[] args) where T : class
        {
            this.LineArguments = args;
            this.CommandLineApplication = new CommandLineApplication(false);
            this.Options = new Dictionary<PropertyInfo, CommandOption>();
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

                this.Options.Add(property, this.CommandLineApplication.Option(argumentOptionAttribute.GetTemplate(property.Name), argumentOptionAttribute.Description, argumentOptionAttribute.Type));
            }

            return this;
        }

        /// <summary>
        /// Sets the console application version.
        /// </summary>
        /// <param name="shortVersion">The short version.</param>
        /// <param name="longVersion">The long version.</param>
        /// <returns></returns>
        public ConsoleHost SetVersion(string shortVersion, string longVersion)
        {
            this.ShortVersion = shortVersion;
            this.LongVersion = longVersion;

            return this;
        }

        /// <summary>
        /// Opens the configuration.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="basePath">The base path.</param>
        /// <returns></returns>
        public ConsoleHost UseConfiguration(string fileName, string basePath = null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath ?? Directory.GetCurrentDirectory())
                .AddJsonFile(fileName);

            this.ConfigurationRoot = builder.Build();

            return this;
        }

        /// <summary>
        /// Setups the startup program.
        /// </summary>
        /// <typeparam name="T">Type of the startup program.</typeparam>
        /// <returns></returns>
        /// <exception cref="System.Exception">The startup couldn't be created.</exception>
        public ConsoleHost UseStartup<T>() where T : IStartup
        {
            if (this.ConfigurationRoot != null)
                this.Startup = Activator.CreateInstance(typeof(T), this.ConfigurationRoot) as IStartup;
            else
                this.Startup = Activator.CreateInstance(typeof(T)) as IStartup;

            if (this.Startup == null)
                throw new Exception("The startup couldn't be created.");

            this.ServiceCollection = new ServiceCollection();
            this.Startup.ConfigureServices(this.ServiceCollection);
            return this;
        }

        /// <summary>
        /// Runs the specified program.
        /// </summary>
        /// <param name="onError">Method called if an error is thrown.</param>
        /// <returns>Execution status code.</returns>
        public int Run(Action<Exception> onError = null)
        {
            try
            {
                if (this.CommandLineApplication != null)
                    return this.ParseCommandLineAndRun(onError);

                this.RunStartup();
                return 0;
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                return -1;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parses the command line and run the program.
        /// </summary>
        /// <param name="onError">The on error.</param>
        /// <returns>Execution status code</returns>
        private int ParseCommandLineAndRun(Action<Exception> onError)
        {
            this.CommandLineApplication.OnExecute(() =>
            {
                try
                {
                    this.ConfigureArguments();
                    this.RunStartup();
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

        /// <summary>
        /// Configures the argument object with the command line arguments.
        /// </summary>
        /// <exception cref="System.Exception">Couldn't create an instance of argument object provided.</exception>
        private void ConfigureArguments()
        {
            if (this.CommandLineApplication == null || this.Options == null)
                return;

            var arguments = Activator.CreateInstance(this.ArgumentsType);

            if (arguments == null)
                throw new Exception("Couldn't create an instance of argument object provided.");

            foreach (var property in this.Options.Keys)
            {
                var option = this.Options[property];

                if (option.OptionType == CommandOptionType.NoValue)
                    continue;

                if (option.OptionType == CommandOptionType.SingleValue)
                    property.SetValue(arguments, Convert.ChangeType(option.Value(), property.PropertyType));
                else
                {
                    var list = Activator.CreateInstance(property.PropertyType) as IList;
                    var listItemType = property.PropertyType.GetGenericArguments().FirstOrDefault();

                    if (list == null)
                        throw new Exception($"Couldn't create the argument list for argument '{property.Name}'.");

                    foreach (var value in option.Values)
                    {
                        list.Add(Convert.ChangeType(value, listItemType));
                    }

                    property.SetValue(arguments, list);
                }
            }

            this.ServiceCollection.AddSingleton(this.ArgumentsType, arguments);
        }

        /// <summary>
        /// Creates the service provider and runs the startup.
        /// </summary>
        private void RunStartup()
        {
            this.ServiceProvider = this.ServiceCollection.BuildServiceProvider();
            this.Startup.Run(this.ServiceProvider);
        }

        #endregion
    }
}
