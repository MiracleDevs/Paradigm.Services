/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paradigm.Services.Repositories
{
    public partial interface IReadRepository<TEntity, in TId>
    {
        Task<TEntity> GetByIdAsync(TId id);

        Task<List<TEntity>> GetAllAsync();
    }
}