/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Threading.Tasks;

namespace Paradigm.Services.Repositories.UOW
{
    public partial interface ICommiteable
    {
        Task CommitChangesAsync();
    }
}