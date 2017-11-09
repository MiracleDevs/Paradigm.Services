using System;
using System.IO;
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
        /// Gets the argument parser.
        /// </summary>
        /// <value>
        /// The argument parser.
        /// </value>
        public ArgumentParser ArgumentParser { get; private set; }

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
            this.ArgumentParser = new ArgumentParser();
            this.ArgumentParser.ParseArguments<T>(args);
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
            if (this.ArgumentParser == null)
                throw new Exception("Please first call the method ParseArguments.");

            this.ArgumentParser.SetVersion(shortVersion, longVersion);
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
                if (this.ArgumentParser != null)
                    return this.ArgumentParser.Run((t, a) => this.ServiceCollection.AddSingleton(t, a), onError);

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