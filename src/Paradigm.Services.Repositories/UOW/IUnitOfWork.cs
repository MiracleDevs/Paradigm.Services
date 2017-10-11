/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

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
