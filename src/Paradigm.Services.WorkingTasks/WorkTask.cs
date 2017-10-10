﻿using System;
using System.Threading.Tasks;
using Paradigm.Services.Exceptions;

namespace Paradigm.Services.WorkingTasks
{
    public partial class WorkTask : IWorkTask
    {
        #region Implementation of IWorkTask

        public void Execute(Action action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var needToRepeat = true;
            var succeed = false;
            var count = 0;
            var exception = new CollectionException();

            while(needToRepeat)
            { 
                try
                {
                    this.BeforeExecute();
                    action();
                    needToRepeat = false;
                    succeed = true;
                    this.AfterExecuteSucceed();
                }
                catch (Exception ex)
                {
                    exception.Add(ex);
                    needToRepeat = repeat < 0 || repeat > count++;
                    this.AfterExecuteFailed();

                    if (waitBeforeRepeat != null)
                    {                     
                       Task.Delay(waitBeforeRepeat.Value).Wait();
                    }
                }
            }

            if (!succeed)
            {
                throw exception;
            }
        }

        public T Execute<T>(Func<T> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
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
                    this.BeforeExecute();
                    result = action();
                    needToRepeat = false;
                    succeed = true;
                    this.AfterExecuteSucceed();
                }
                catch (Exception ex)
                {
                    exception.Add(ex);
                    needToRepeat = repeat < 0 || repeat > count++;
                    this.AfterExecuteFailed();

                    if (waitBeforeRepeat != null)
                    {
                        Task.Delay(waitBeforeRepeat.Value).Wait();
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

        protected virtual void BeforeExecute()
        {
        }

        protected virtual void AfterExecuteSucceed()
        {
        }

        protected virtual void AfterExecuteFailed()
        {
        }

        #endregion
    }
}
