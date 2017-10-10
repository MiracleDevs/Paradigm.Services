using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.Services.Repositories.UOW
{
    public partial class UnitOfWork
    {
        #region Implementation of IUnitOfWork

        public async Task CommitChangesAsync()
        {
            var commiteable = this.Repositories.Where(x => x is ICommiteable).Cast<ICommiteable>();

            foreach(var repository in commiteable)
            {
                await repository.CommitChangesAsync();
            }
        }

        #endregion
    }
}
