using System;
using System.Collections.Generic;

namespace Paradigm.Services.WorkingTasks
{
    /// <summary>
    /// Provides an interface for an object that allows to execute a task in parallel.
    /// </summary>
    public interface IParallelWorkTask
    {
        /// <summary>
        /// Executes concurrently the provided action for each element in the source enumeration
        /// </summary>
        /// <param name="source">Source enumeration</param>
        /// <param name="action">Action to concurrently apply to each element of the source enumeration</param>
        /// <param name="maxDegreeOfParallelism">0 to use the instance default value, a positive number representing the number of max. concurrent operations or -1 if there is no limit</param>
        void Execute<T>(IEnumerable<T> source, Action<T> action, int maxDegreeOfParallelism = 0);

        /// <summary>
        /// Seperates the source enumeration in multiple batches each containing up to N elements, where N is configurable trough the parameter <see cref="elementsPerBatch"/>,
        /// and then executes concurrently the provided action for each batch.
        /// </summary>
        /// <param name="source">Source enumeration</param>
        /// <param name="elementsPerBatch">Maximum number of elements per batch</param>
        /// <param name="action">Action to concurrently apply to each element of the source enumeration</param>
        /// <param name="maxDegreeOfParallelism">0 to use the instance default value, a positive number representing the number of max. concurrent operations or -1 if there is no limit</param>
        void ExecuteAsBatch<T>(IEnumerable<T> source, int elementsPerBatch, Action<List<T>> action, int maxDegreeOfParallelism = 0);
    }
}
