/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Threading.Tasks;
using Paradigm.Services.WorkingTasks;

namespace Paradigm.Services.Mvc.ORM.Controllers
{
    public abstract partial class ApiControllerBase
    {
        #region Protected Methods

        protected async Task TaskAsync(Func<Task> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            await this.Resolve<IWorkTask>().ExecuteAsync(action, repeat, waitBeforeRepeat);
        }

        protected async Task<T> TaskAsync<T>(Func<Task<T>> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            return await this.Resolve<IWorkTask>().ExecuteAsync(action, repeat, waitBeforeRepeat);
        }

        protected async Task TransactionalTaskAsync(Func<Task> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            using (var task = this.Resolve<ITransactionalWorkTask>())
            {
                await task.ExecuteAsync(action, repeat, waitBeforeRepeat);
            }
        }

        protected async Task<T> TransactionalTaskAsync<T>(Func<Task<T>> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            using (var task = this.Resolve<ITransactionalWorkTask>())
            {
                return await task.ExecuteAsync(action, repeat, waitBeforeRepeat);
            }
        }

        #endregion
    }
}