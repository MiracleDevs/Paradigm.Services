using System.Threading.Tasks;

namespace Paradigm.Services.Repositories.UOW
{
    public partial interface IUnitOfWork
    {
        Task CommitChangesAsync();
    }
}
