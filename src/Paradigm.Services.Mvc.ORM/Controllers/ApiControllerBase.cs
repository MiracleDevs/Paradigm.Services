using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Exceptions;
using Paradigm.Services.Providers;
using Paradigm.Services.Repositories.UOW;
using Paradigm.Services.WorkingTasks;
using Paradigm.Services.WorkingTasks.ORM;

namespace Paradigm.Services.Mvc.ORM.Controllers
{
    public abstract partial class ApiControllerBase : Controller
    {
        #region Properties

        protected IServiceProvider ServiceProvider { get; }

        protected IUnitOfWork UnitOfWork { get; }

        protected IExceptionHandler ExceptionHandler { get; }

        #endregion

        #region Constructor

        protected ApiControllerBase(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IExceptionHandler exceptionHandler)
        {
            this.ServiceProvider = serviceProvider;
            this.UnitOfWork = unitOfWork;
            this.ExceptionHandler = exceptionHandler;
        }

        #endregion

        #region Protected Methods

        protected void Task(Action action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            this.Resolve<IWorkTask>().Execute(action, repeat, waitBeforeRepeat);
        }

        protected T Task<T>(Func<T> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            return this.Resolve<IWorkTask>().Execute(action, repeat, waitBeforeRepeat);
        }

        protected void TransactionalTask(Action action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            using (var task = this.Resolve<ITransactionalWorkTask>())
            {
                task.Execute(action, repeat, waitBeforeRepeat);
            }
        }

        protected T TransactionalTask<T>(Func<T> action, int repeat = 0, TimeSpan? waitBeforeRepeat = null)
        {
            using (var task = this.Resolve<ITransactionalWorkTask>())
            {
                return task.Execute(action, repeat, waitBeforeRepeat);
            }
        }

        protected TProvider GetProvider<TProvider>() where TProvider : IProvider
        {
            return this.ServiceProvider.GetService<TProvider>();
        }

        protected T Resolve<T>() where T: class
        {
            return this.ServiceProvider.GetService<T>();
        }

        #endregion
    }
}