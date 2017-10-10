using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Paradigm.Services.WorkingTasks;

namespace Paradigm.Services.Providers
{
    public abstract partial class ReadProviderBase<TView, TViewRepository, TId>
    {
        #region Public Methods

        public virtual async Task<List<TView>> FindViewAsync()
        {
            return await this.GetViewRepository().GetAllAsync();
        }

        public virtual async Task<TView> GetViewAsync(TId id)
        {
            return this.CheckEntity(await this.GetViewRepository().GetByIdAsync(id));
        }

        #endregion

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