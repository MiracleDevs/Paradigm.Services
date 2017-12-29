namespace Paradigm.Services.CLI
{
    /// <summary>
    /// Provides information about the console hosting environment an application is running in.
    /// </summary>
    public interface IHostingEnvironment
    {
        /// <summary>
        /// Gets the absolute path to the directory that contains the application content files.
        /// </summary>
        string ContentRootPath { get; }

        /// <summary>
        /// Gets the name of the environment. This property is automatically set by the host to the value of the "NETCORE_CONSOLE_ENVIRONMENT" environment variable.
        /// </summary>
        string EnvironmentName { get; }
    }
}