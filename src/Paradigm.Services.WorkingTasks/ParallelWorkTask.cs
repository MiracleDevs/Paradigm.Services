using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigm.Services.WorkingTasks
{
    public class ParallelWorkTask : IParallelWorkTask
    {
        /// <summary>
        /// Default degree of parallelism for this work action. Accepted valus are:
        ///  -1: there is no limit on the number of concurrently running operations
        /// > 0: Max number of concurrent running operations
        /// </summary>
        protected int MaxDegreeOfParallelism { get; set; }
        
        public ParallelWorkTask(int maxDegreeOfParallelism = -1)
        {
            if (maxDegreeOfParallelism == 0 || maxDegreeOfParallelism < -1)
                throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), "The degree of parallelism must be -1 if there is no limit on the number of concurrent running operations or a positive number");

            this.MaxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        /// <summary>
        /// Executes concurrently the provided action for each element in the source enumeration
        /// </summary>
        /// <param name="source">Source enumeration</param>
        /// <param name="maxDegreeOfParallelism">0 to use the instance default value, a positive number representing the number of max. concurrent operations or -1 if there is no limit</param>
        /// <param name="action">Action to concurrently apply to each element of the source enumeration</param>
        public void Execute<T>(IEnumerable<T> source, int maxDegreeOfParallelism, Action<T> action)
        {
            if (maxDegreeOfParallelism < -1)
                throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), "The degree of parallelism must be -1 if there is no limit on the number of concurrent running operations or a positive number");

            Parallel.ForEach(source, new ParallelOptions()
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism == 0 ? this.MaxDegreeOfParallelism : maxDegreeOfParallelism
            }, action);
        }

        /// <summary>
        /// Seperates the source enumeration in multiple batches each containing up to N elements, where N is configurable trough the parameter <see cref="elementsPerBatch"/>, 
        /// and then executes concurrently the provided action for each batch.
        /// </summary>
        /// <param name="source">Source enumeration</param>
        /// <param name="elementsPerBatch">Maximum number of elements per batch</param>
        /// <param name="maxDegreeOfParallelism">0 to use the instance default value, a positive number representing the number of max. concurrent operations or -1 if there is no limit</param>
        /// <param name="action">Action to concurrently apply to each element of the source enumeration</param>
        public void ExecuteAsBatch<T>(IEnumerable<T> source, int elementsPerBatch, int maxDegreeOfParallelism, Action<List<T>> action)
        {
            if (elementsPerBatch <= 0)
                throw new ArgumentOutOfRangeException(nameof(elementsPerBatch), "The number of elements per batch must be a positive number");

            if (maxDegreeOfParallelism < -1)
                throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), "The degree of parallelism must be 0 to use the default value, -1 if there is no limit on the number of concurrent running operations or a positive number");

            var enumerable = source as IList<T> ?? source.ToList();
            List<List<T>> batch = enumerable.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / elementsPerBatch)
                .Select(x => x.Select(v => v.Value).ToList()).ToList();

            Parallel.ForEach(batch, new ParallelOptions()
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism == 0 ? this.MaxDegreeOfParallelism : maxDegreeOfParallelism
            }, action);
        }
    }
}
