using System;
using System.Collections.Generic;
using Paradigm.Services.Domain;
using Paradigm.Services.Exceptions;
using Paradigm.Services.Repositories;
using Paradigm.Services.Repositories.UOW;
using Paradigm.Services.WorkingTasks;

namespace Paradigm.Services.Providers
{
    public abstract partial class ReadProviderBase<TView, TViewRepository, TId> : ProviderBase, IReadProvider<TView, TId>
        where TView : DomainBase
        where TViewRepository : IReadRepository<TView, TId>
    {
        #region Constructor

        protected ReadProviderBase(IServiceProvider serviceProvider, IUnitOfWork unitOfWork) : base(serviceProvider, unitOfWork)
        {
        }

        #endregion

        #region Public Methods

        public virtual List<TView> FindView()
        {
            return this.GetViewRepository().GetAll();
        }

        public virtual TView GetView(TId id)
        {
            return this.CheckEntity(this.GetViewRepository().GetById(id));
        }

        #endregion

        #region Protected Methods

        protected virtual TViewRepository GetViewRepository()
        {
            return this.GetRepository<TViewRepository>();
        }

        protected virtual TView CheckEntity(TView view)
        {
            if (view == null)
            {
                throw new NotFoundException(typeof(TView));
            }

            return view;
        }

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

        #endregion

        #endregion
    }
}