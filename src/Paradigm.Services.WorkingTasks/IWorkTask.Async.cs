using System;
using System.Threading.Tasks;

namespace Paradigm.Services.WorkingTasks
{
    public partial interface IWorkTask
    {
        Task ExecuteAsync(Func<Task> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null);

        Task<T> ExecuteAsync<T>(Func<Task<T>> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null);
    }
}
