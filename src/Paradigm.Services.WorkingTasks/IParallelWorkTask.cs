using System;
using System.Collections.Generic;
using System.Text;

namespace Paradigm.Services.WorkingTasks
{
    public interface IParallelWorkTask
    {
        void Execute<T>(IEnumerable<T> source, int maxDegreeOfParallelism, Action<T> action);

        void ExecuteAsBatch<T>(IEnumerable<T> source, int elementsPerBatch, int maxDegreeOfParallelism, Action<List<T>> action);
    }
}
