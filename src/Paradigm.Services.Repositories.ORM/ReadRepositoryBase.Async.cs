using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paradigm.Services.Repositories.ORM
{
    public abstract partial class ReadRepositoryBase<TEntity, TId, TDatabaseAccess>
    {
        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await this.DatabaseAccess.SelectOneAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await this.DatabaseAccess.SelectAsync();
        }
    }
}