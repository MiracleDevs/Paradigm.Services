using System;
using System.IO;
using System.Reflection;

namespace Paradigm.Services.CLI
{
    /// <summary>
    /// Provides values for the console hosting environment.
    /// </summary>
    /// <seealso cref="Paradigm.Services.CLI.IHostingEnvironment" />
    internal class ConsoleHostingEnvironment: IHostingEnvironment
    {
        /// <summary>
        /// The environment name key.
        /// </summary>
        public const string EnvironmentNameKey = "NETCORE_CONSOLE_ENVIRONMENT";

        /// <summary>
        /// Gets the absolute path to the directory that contains the application content files.
        /// </summary>
        public string ContentRootPath => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        /// <summary>
        /// Gets the name of the environment. This property is automatically set by the host to the value of the "NETCORE_CONSOLE_ENVIRONMENT" environment variable.
        /// </summary>
        public string EnvironmentName => Environment.GetEnvironmentVariable(EnvironmentNameKey);
    }
}