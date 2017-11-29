using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.Services.WorkingTasks
{
    /// <summary>
    /// Executes a work task in parallel, allowing for a batch or single execution.
    /// </summary>
    public class ParallelWorkTask : IParallelWorkTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the maximum number of concurrent tasks enabled by this instance.
        /// </summary>
        /// <remarks>
        /// Default degree of parallelism for this work action. Accepted valus are:
        ///  -1: there is no limit on the number of concurrently running operations
        /// > 0: Max number of concurrent running operations
        /// </remarks>
        protected int MaxDegreeOfParallelism { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelWorkTask"/> class.
        /// </summary>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">maxDegreeOfParallelism - The degree of parallelism must be -1 if there is no limit on the number of concurrent running operations or a positive number</exception>
        public ParallelWorkTask(int maxDegreeOfParallelism = -1)
        {
            if (maxDegreeOfParallelism == 0 || maxDegreeOfParallelism < -1)
                throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), "The maximum degree of parallelism needs to be a positive integer, or -1 to use the maximum allowed by the system.");

            this.MaxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes concurrently the provided action for each element in the source enumeration
        /// </summary>
        /// <param name="source">Source enumeration</param>
        /// <param name="action">Action to concurrently apply to each element of the source enumeration</param>
        /// <param name="maxDegreeOfParallelism">0 to use the instance default value, a positive number representing the number of max. concurrent operations or -1 if there is no limit</param>
        public void Execute<T>(IEnumerable<T> source, Action<T> action, int maxDegreeOfParallelism = 0)
        {
            Parallel.ForEach(source, new ParallelOptions
            {
                MaxDegreeOfParallelism = this.GetMaxDegreeOfParallelism(maxDegreeOfParallelism)
            }, action);
        }

        /// <summary>
        /// Seperates the source enumeration in multiple batches each containing up to N elements, where N is configurable trough the parameter <see cref="elementsPerBatch"/>,
        /// and then executes concurrently the provided action for each batch.
        /// </summary>
        /// <param name="source">Source enumeration</param>
        /// <param name="elementsPerBatch">Maximum number of elements per batch</param>
        /// <param name="action">Action to concurrently apply to each element of the source enumeration</param>
        /// <param name="maxDegreeOfParallelism">0 to use the instance default value, a positive number representing the number of max. concurrent operations or -1 if there is no limit</param>
        public void ExecuteAsBatch<T>(IEnumerable<T> source, int elementsPerBatch, Action<List<T>> action, int maxDegreeOfParallelism = 0)
        {
            if (elementsPerBatch <= 0)
                throw new ArgumentOutOfRangeException(nameof(elementsPerBatch), "The number of elements per batch must be a positive number.");

            var enumerable = source as IList<T> ?? source.ToList();
            var batch = enumerable.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / elementsPerBatch)
                .Select(x => x.Select(v => v.Value).ToList()).ToList();

            Parallel.ForEach(batch, new ParallelOptions
            {
                MaxDegreeOfParallelism = GetMaxDegreeOfParallelism(maxDegreeOfParallelism)
            }, action);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the maximum degree of parallelism.
        /// </summary>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxDegreeOfParallelism - The maximum degree of parallelism needs to be a positive integer, or -1 to use the maximum allowed by the system.</exception>
        private int GetMaxDegreeOfParallelism(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < -1)
                throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), "The maximum degree of parallelism needs to be a positive integer, or -1 to use the maximum allowed by the system.");

            return maxDegreeOfParallelism == 0
                            ? this.MaxDegreeOfParallelism
                            : maxDegreeOfParallelism;
        }

        #endregion
    }
}
