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