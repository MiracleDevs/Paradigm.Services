using System;

namespace Paradigm.Services.WorkingTasks
{
    public partial interface IWorkTask
    {
        void Execute(Action action, int repeat = 0, TimeSpan? waitBeforeRepeat = null);

        T Execute<T>(Func<T> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null);
    }
}
