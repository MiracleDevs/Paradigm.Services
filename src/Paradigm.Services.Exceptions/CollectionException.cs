/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Paradigm.Services.Exceptions
{
    /// <summary>
    /// Represents a collection of errors that occurs during the execution of a given process.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class CollectionException : Exception
    {
        /// <summary>
        /// Gets a readonly collection of exceptions.
        /// </summary>
        public IReadOnlyCollection<Exception> Exceptions { get; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => string.Join(Environment.NewLine, this.Exceptions.Select(x => x.Message));

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionException"/> class.
        /// </summary>
        public CollectionException()
        {
            this.Exceptions = new List<Exception>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionException"/> class.
        /// </summary>
        /// <param name="ex">A collection of exceptions.</param>
        public CollectionException(IEnumerable<Exception> ex)
        {
            this.Exceptions = new List<Exception>(ex);
        }

        /// <summary>
        /// Adds a new exception ot the collection.
        /// </summary>
        /// <param name="ex">The exception to be added.</param>
        public void Add(Exception ex)
        {
            (this.Exceptions as List<Exception>)?.Add(ex);
        }
    }
}
