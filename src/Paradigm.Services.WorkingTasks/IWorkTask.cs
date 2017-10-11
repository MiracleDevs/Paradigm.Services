/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;

namespace Paradigm.Services.WorkingTasks
{
    public partial interface IWorkTask
    {
        void Execute(Action action, int repeat = 0, TimeSpan? waitBeforeRepeat = null);

        T Execute<T>(Func<T> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null);
    }
}
