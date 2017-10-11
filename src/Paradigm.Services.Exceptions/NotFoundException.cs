/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;

namespace Paradigm.Services.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when a given entity or resource was not found.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NotFoundException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotFoundException(string message): base(message)
        {         
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="type">The type of the resource that was not found. Can be a domain entity name.</param>
        public NotFoundException(Type type) : base($"The {type.Name} couldn't be found.")
        {
        }
    }
}