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