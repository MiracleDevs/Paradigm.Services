using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public object Startup { get; private set; }

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
        /// <param name="optional">Whether the file is optional</param>
        /// <param name="reloadOnChange">Whether the configuration should be reloaded if the file changes</param>
        /// <returns></returns>
        public ConsoleHost UseConfiguration(string fileName, string basePath = null, bool optional = false, bool reloadOnChange = true)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath ?? Directory.GetCurrentDirectory())
                .AddJsonFile(fileName, optional, reloadOnChange);

            this.ConfigurationRoot = builder.Build();

            return this;
        }

        /// <summary>
        /// Setups the startup program.
        /// </summary>
        /// <typeparam name="T">Type of the startup program.</typeparam>
        /// <returns></returns>
        /// <exception cref="System.Exception">The startup couldn't be created.</exception>
        public ConsoleHost UseStartup<T>()
        {
            var type = typeof(T);
            var constructorInfo = type.GetConstructor(new[] { typeof(IConfigurationRoot) }) ??
                                  type.GetConstructor(new[] { typeof(IConfiguration) }) ??
                                  type.GetConstructor(new Type[] { });

            if (constructorInfo == null)
                throw new Exception($"Couldn't find a suitable constructor for the startup class '{type.Name}'.");

            this.Startup = constructorInfo.Invoke(constructorInfo.GetParameters().Any()
                ? new object[] { this.ConfigurationRoot }
                : new object[0]);

            if (this.Startup == null)
                throw new Exception("The startup couldn't be created.");

            this.ServiceCollection = new ServiceCollection();

            if (this.ArgumentParser?.Arguments != null)
                this.ServiceCollection.AddSingleton(this.ArgumentParser.ArgumentsType, this.ArgumentParser.Arguments);

            var configureMethodInfo = type.GetMethod("ConfigureServices", new[] { typeof(IServiceCollection) });
            configureMethodInfo?.Invoke(this.Startup, new object[] { this.ServiceCollection });

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
                if(this.Startup == null)
                    throw new Exception("The startup class couldn't be constructed and can not run.");

                this.ServiceProvider = this.ServiceCollection.BuildServiceProvider();
                var runMethodInfo = this.Startup.GetType().GetMethod("Run", new []{ typeof(IServiceProvider) });

                if (runMethodInfo == null)
                    throw new Exception($"Couldn't find a suitable run method for the startup class '{this.Startup.GetType().Name}'.");

                if (runMethodInfo.ReturnType == typeof(Task))
                {
                    ((Task)runMethodInfo.Invoke(this.Startup, new object[] { this.ServiceProvider })).Wait();
                }
                else
                {
                    runMethodInfo.Invoke(this.Startup, new object[] { this.ServiceProvider });
                }

                return 0;
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                return -1;
            }
        }

        #endregion
    }
}