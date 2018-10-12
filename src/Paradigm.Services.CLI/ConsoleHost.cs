using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// Gets the hosting environment.
        /// </summary>
        /// <value>
        /// The hosting environment.
        /// </value>
        public IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// Gets the service collection.
        /// </summary>
        /// <value>
        /// The service collection.
        /// </value>
        public IServiceCollection ServiceCollection { get; }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Gets the configuration builder.
        /// </summary>
        /// <value>
        /// The configuration builder.
        /// </value>
        public IConfigurationBuilder ConfigurationBuilder { get; private set; }

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

        /// <summary>
        /// Gets the startup program.
        /// </summary>
        /// <value>
        /// The startup program.
        /// </value>
        public object Startup { get; private set; }

        /// <summary>
        /// Gets the startup program type.
        /// </summary>
        /// <value>
        /// The startup program type.
        /// </value>
        public Type StartupType { get; private set; }

        /// <summary>
        /// Gets the error handler.
        /// </summary>
        /// <value>
        /// The error handler.
        /// </value>
        public Action<Exception> ErrorHandler { get; private set; }

        /// <summary>
        /// Gets the exit handler.
        /// </summary>
        /// <value>
        /// The exit handler.
        /// </value>
        public Action ExitHandler { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="ConsoleHost"/> class from being created.
        /// </summary>
        private ConsoleHost()
        {
            this.HostingEnvironment = new ConsoleHostingEnvironment();
            this.ServiceCollection = new ServiceCollection();
            this.ServiceCollection.AddSingleton(this.HostingEnvironment);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns>A reference to the console host instance.</returns>
        public static ConsoleHost Create()
        {
            return new ConsoleHost();
        }

        /// <summary>
        /// Parses the console line arguments.
        /// </summary>
        /// <typeparam name="T">A type decorated with the options</typeparam>
        /// <param name="args">The console line arguments arguments.</param>
        /// <returns>A reference to the argument parser.</returns>
        public ArgumentParser ParseArguments<T>(string[] args) where T : class
        {
            this.ArgumentParser = new ArgumentParser();
            this.ArgumentParser.ParseArguments<T>(args);

            if (this.ArgumentParser?.Arguments != null)
                this.ServiceCollection.AddSingleton(this.ArgumentParser.ArgumentsType, this.ArgumentParser.Arguments);

            return this.ArgumentParser;
        }

        /// <summary>
        /// Uses the configuration.
        /// </summary>
        /// <returns>A reference to the configuration builder.</returns>
        public IConfigurationBuilder UseConfiguration()
        {
            return this.ConfigurationBuilder = new ConfigurationBuilder().SetBasePath(this.HostingEnvironment.ContentRootPath);
        }

        /// <summary>
        /// Setups the startup program.
        /// </summary>
        /// <typeparam name="T">Type of the startup program.</typeparam>
        /// <returns>A reference to the console host.</returns>
        public ConsoleHost UseStartup<T>()
        {
            this.StartupType = typeof(T);
            return this;
        }

        /// <summary>
        /// Sets an error handler at application domain level
        /// </summary>
        /// <param name="errorHandler">The error handler.</param>
        /// <returns>A reference to the console host.</returns>
        /// <exception cref="ArgumentNullException">errorHandler - The error handler can not be null.</exception>
        public ConsoleHost HandleErrors(Action<Exception> errorHandler)
        {
            this.ErrorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler), "The error handler can not be null.");
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => this.ErrorHandler(args.ExceptionObject as Exception);
            TaskScheduler.UnobservedTaskException += (sender, args) => this.ErrorHandler(args.Exception);
            return this;
        }

        /// <summary>
        /// Sets an exit handler at application domain level.
        /// </summary>
        /// <param name="exitHandler">The exit handler.</param>
        /// <returns>A reference to the console host.</returns>
        /// <exception cref="ArgumentNullException">exitHandler - The exit handler can not be null.</exception>
        public ConsoleHost HandleExit(Action exitHandler)
        {
            this.ExitHandler = exitHandler ?? throw new ArgumentNullException(nameof(exitHandler), "The exit handler can not be null.");
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => this.ExitHandler();
            return this;
        }

        /// <summary>
        /// Builds and run the specified program.
        /// </summary>
        /// <returns>A reference to the console host.</returns>
        public ConsoleHost Build()
        {
            this.BuildConfiguration();
            this.BuildStartup();
            this.BuildServiceProvider();
            this.RunStartup();

            return this;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        private void BuildConfiguration()
        {
            this.ConfigurationRoot = this.ConfigurationBuilder?.Build();
        }

        /// <summary>
        /// Builds the startup.
        /// </summary>
        private void BuildStartup()
        {
            var constructorInfo = this.StartupType.GetConstructor(new[] { typeof(IConfigurationRoot) }) ??
                                  this.StartupType.GetConstructor(new[] { typeof(IConfiguration) }) ??
                                  this.StartupType.GetConstructor(new Type[] { });

            if (constructorInfo == null)
                throw new Exception($"Couldn't find a suitable constructor for the startup class '{this.StartupType.Name}'.");

            this.Startup = constructorInfo.Invoke(constructorInfo.GetParameters().Any()
                ? new object[] { this.ConfigurationRoot }
                : new object[0]);

            if (this.Startup == null)
                throw new Exception("The startup couldn't be created.");

            var configureMethodInfo = this.StartupType.GetMethod("ConfigureServices", new[] { typeof(IServiceCollection) });
            configureMethodInfo?.Invoke(this.Startup, new object[] { this.ServiceCollection });
        }

        /// <summary>
        /// Builds the service provider.
        /// </summary>
        private void BuildServiceProvider()
        {
            this.ServiceProvider = this.ServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Runs the startup.
        /// </summary>
        private void RunStartup()
        {
            var runMethodInfo = this.Startup.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.Name == "Run");

            if (runMethodInfo == null)
                throw new Exception($"Couldn't find a suitable run method for the startup class '{this.Startup.GetType().Name}'.");

            if (runMethodInfo.ReturnType == typeof(Task))
            {
                ((Task)runMethodInfo.Invoke(this.Startup, this.ResolveParameters(runMethodInfo.GetParameters()))).Wait();
            }
            else
            {
                runMethodInfo.Invoke(this.Startup, this.ResolveParameters(runMethodInfo.GetParameters()));
            }
        }

        private object[] ResolveParameters(IReadOnlyList<ParameterInfo> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return new object[0];

            var result = new object[parameters.Count];

            for (var index = 0; index < parameters.Count; index++)
            {
                var parameter = parameters[index];
                result[index] = this.ServiceProvider.GetService(parameter.ParameterType);
            }

            return result;
        }

        #endregion
    }
}