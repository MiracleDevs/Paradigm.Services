using System;
using System.Collections.Generic;
using System.Linq;

namespace Paradigm.Services.Repositories.UOW
{
    public partial class UnitOfWork : IUnitOfWork
    {
        #region Properties

        protected List<IRepository> Repositories { get; }

        public bool Disposing { get; private set; }

        #endregion

        #region Constructor

        public UnitOfWork()
        {
            this.Repositories = new List<IRepository>();
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (this.Disposing)
                return;

            this.Disposing = true;

            this.Reset();
        }

        #endregion

        #region Implementation of IUnitOfWork

        public void Reset()
        {
            foreach (var repository in this.Repositories)
            {
                repository.Dispose();
            }

            this.Repositories.Clear();
        }

        public void RegisterRepository(IRepository repository)
        {
            if (this.Disposing)
                throw new InvalidOperationException("Can't add a Repository to a Unit Of Work while the Unit of Work is being disposed.");

            this.Repositories.Add(repository);
        }

        public void CommitChanges()
        {
            var commiteable = this.Repositories.Where(x => x is ICommiteable).Cast<ICommiteable>();

            foreach(var repository in commiteable)
            {
                repository.CommitChanges();
            }
        }

        #endregion
    }
}
