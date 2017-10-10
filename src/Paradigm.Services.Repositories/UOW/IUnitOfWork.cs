using System;

namespace Paradigm.Services.Repositories.UOW
{
    public partial interface IUnitOfWork: IDisposable
    {
        void RegisterRepository(IRepository repository);

        void CommitChanges();

        void Reset();
    }
}
