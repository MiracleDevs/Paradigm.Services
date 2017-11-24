/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Paradigm.Services.Repositories.UOW
{
    public partial class UnitOfWork : IUnitOfWork
    {
        #region Properties

        /// <summary>
        /// Gets the list of current repositories registered
        /// in the current UnitOfWork
        /// </summary>
        protected List<IRepository> Repositories { get; }

        /// <summary>
        /// Flag to track if this UnitOfWork is being disposed
        /// </summary>
        public bool Disposing { get; private set; }

        #endregion

        #region Private Fields

        /// <summary>
        /// Lock used to make the list of repositories thread-safe
        /// </summary>
        private readonly object _repositoriesLock = new object();

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
            lock (_repositoriesLock)
            {
                foreach (var repository in this.Repositories)
                {
                    repository.Dispose();
                }

                this.Repositories.Clear();
            }
        }

        public void RegisterRepository(IRepository repository)
        {
            if (this.Disposing)
                throw new InvalidOperationException("Can't add a Repository to a Unit Of Work while the Unit of Work is being disposed.");

            lock (_repositoriesLock)
                this.Repositories.Add(repository);
        }

        public void CommitChanges()
        {
            // By now locking all the repositories prevents parallel CommitChanges (revisit this later)
            lock (_repositoriesLock)
            {
                var commiteable = this.Repositories.Where(x => x is ICommiteable).Cast<ICommiteable>();

                foreach (var repository in commiteable)
                {
                    repository.CommitChanges();
                }
            }
        }

        #endregion
    }
}
