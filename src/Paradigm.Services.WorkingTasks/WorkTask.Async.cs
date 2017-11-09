/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Threading.Tasks;
using Paradigm.Services.Exceptions;

namespace Paradigm.Services.WorkingTasks
{
    public partial class WorkTask
    {
        #region Implementation of IWorkTask

        public async Task ExecuteAsync(Func<Task> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var needToRepeat = true;
            var succeed = false;
            var count = 0;
            var exception = new CollectionException();

            while (needToRepeat)
            {
                try
                {
                    await this.BeforeExecuteAsync();
                    await action();
                    needToRepeat = false;
                    succeed = true;
                    await this.AfterExecuteSucceedAsync();
                }
                catch (Exception ex)
                {
                    if (repeat >= 0)
                    {
                        // we avoid adding the exception
                        // in case of repeat until works,
                        // to prevent an escenario where
                        // the exception list grows indefinitely.
                        exception.Add(ex);
                    }

                    needToRepeat = repeat < 0 || repeat > count++;
                    await this.AfterExecuteFailedAsync();

                    if (waitBeforeRepeat != null)
                    {
                        await Task.Delay(waitBeforeRepeat.Value);
                    }
                }
            }

            if (!succeed)
            {
                throw exception;
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var needToRepeat = true;
            var succeed = false;
            var count = 0;
            var result = default(T);
            var exception = new CollectionException();

            while (needToRepeat)
            {
                try
                {
                    await this.BeforeExecuteAsync();
                    result = await action();
                    needToRepeat = false;
                    succeed = true;
                    await this.AfterExecuteSucceedAsync();
                }
                catch (Exception ex)
                {
                    if (repeat >= 0)
                    {
                        // we avoid adding the exception
                        // in case of repeat until works,
                        // to prevent an escenario where
                        // the exception list grows indefinitely.
                        exception.Add(ex);
                    }

                    needToRepeat = repeat < 0 || repeat > count++;
                    await this.AfterExecuteFailedAsync();

                    if (waitBeforeRepeat != null)
                    {
                        await Task.Delay(waitBeforeRepeat.Value);
                    }
                }
            }

            if (!succeed)
            {
                throw exception;
            }

            return result;
        }

        #endregion

        #region Protected Methods

        protected virtual Task BeforeExecuteAsync()
        {
            return Task.FromResult(default(object));
        }

        protected virtual Task AfterExecuteSucceedAsync()
        {
            return Task.FromResult(default(object));
        }

        protected virtual Task AfterExecuteFailedAsync()
        {
            return Task.FromResult(default(object));
        }

        #endregion
    }
}
