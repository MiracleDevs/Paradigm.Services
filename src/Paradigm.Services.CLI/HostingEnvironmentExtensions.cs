namespace Paradigm.Services.CLI
{
    /// <summary>
    /// Provides extension methods for IHostingEnvironment.
    /// </summary>
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        /// The NETCORE_CONSOLE_ENVIRONMENT development key.
        /// </summary>
        public const string DevelopmentKey = "Development";

        /// <summary>
        /// The NETCORE_CONSOLE_ENVIRONMENT staging key.
        /// </summary>
        public const string StagingKey = "Staging";

        /// <summary>
        /// The NETCORE_CONSOLE_ENVIRONMENT production key
        /// </summary>
        public const string ProductionKey = "Production";

        /// <summary>
        /// Determines whether the specified environment is a given environment.
        /// </summary>
        /// <param name="hosting">The hosting.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>
        ///   <c>true</c> if the specified environment is environment; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEnvironment(this IHostingEnvironment hosting, string environment) => hosting?.EnvironmentName == environment;

        /// <summary>
        /// Determines whether the current environment is a development environment.
        /// </summary>
        /// <param name="hosting">The hosting.</param>
        /// <returns>
        ///   <c>true</c> if the specified hosting is development; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDevelopment(this IHostingEnvironment hosting) => IsEnvironment(hosting, DevelopmentKey);

        /// <summary>
        /// Determines whether the current environment is an staging environment.
        /// </summary>
        /// <param name="hosting">The hosting.</param>
        /// <returns>
        ///   <c>true</c> if the specified hosting is development; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStaging(this IHostingEnvironment hosting) => IsEnvironment(hosting, StagingKey);

        /// <summary>
        /// Determines whether the current environment is a production environment.
        /// </summary>
        /// <param name="hosting">The hosting.</param>
        /// <returns>
        ///   <c>true</c> if the specified hosting is development; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProduction(this IHostingEnvironment hosting) => IsEnvironment(hosting, ProductionKey);
    }
}