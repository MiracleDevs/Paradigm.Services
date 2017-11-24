/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.Services.Repositories.UOW
{
    public partial class UnitOfWork
    {
        #region Implementation of IUnitOfWork

        public async Task CommitChangesAsync()
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
            await Task.CompletedTask;
        }

        #endregion
    }
}
