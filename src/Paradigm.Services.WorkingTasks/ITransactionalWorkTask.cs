using System;

namespace Paradigm.Services.WorkingTasks
{
    public interface ITransactionalWorkTask : IWorkTask, IDisposable
    {
        
    }
}