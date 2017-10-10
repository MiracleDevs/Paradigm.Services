using System;
using System.Resources;
using Paradigm.Services.Interfaces.Attributes;

namespace Paradigm.Services.Interfaces.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ValidationAttribute" />
    /// </summary>
    public static class ValidationAttributeExtensions
    {
        /// <summary>
        /// Gets or sets the last type of resource.
        /// </summary>
        /// <remarks>
        /// We keep track of the last resource manager used to avoid
        /// create the resource manager each time the format method is called.
        /// Technically, the domain validation will use the same resource file
        /// most of the times, and so this results in only one creation.
        /// In the worst scenario, that each property of a type use a different resource manager,
        /// is practically the same as no cache at all.
        /// </remarks>
        private static Type LastResourceType { get; set; }

        /// <summary>
        /// Gets or sets the last resource manager used.
        /// </summary>
        private static ResourceManager ResourceManager { get; set; }

        /// <summary>
        /// Gets the resource locker.
        /// </summary>
        private static readonly object ResourceLocker = new object();

        /// <summary>
        /// Formats the error message.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The validation error already formatted.</returns>
        public static string FormatErrorMessage(this ValidationAttribute validation, params object[] parameters)
        {
            string text;

            if (validation.ResourceType == null)
            {
                text = validation.ErrorMessage;
            }
            else
            {
                if (LastResourceType == null || ResourceManager == null ||
                    LastResourceType != validation.ResourceType)
                {
                    lock (ResourceLocker)
                    {
                        LastResourceType = validation.ResourceType;
                        ResourceManager = new ResourceManager(LastResourceType);
                    }
                }

                text = ResourceManager.GetString(validation.ResourceName);
            }

            return text != null ? string.Format(text, parameters) : string.Empty;
        }
    }
}