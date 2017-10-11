/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paradigm.Services.Repositories
{
    public partial interface IEditRepository<TEntity, in TId>
    {
        Task AddAsync(TEntity entity);

        Task AddAsync(IEnumerable<TEntity> entity);

        Task EditAsync(TEntity entity);

        Task EditAsync(IEnumerable<TEntity> entity);

        Task RemoveAsync(TEntity entity);

        Task RemoveAsync(IEnumerable<TEntity> entity);
    }
}