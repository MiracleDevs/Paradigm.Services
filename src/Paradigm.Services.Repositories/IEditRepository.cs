using System.Collections.Generic;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Repositories
{
    public partial interface IEditRepository<TEntity, in TId>: IReadRepository<TEntity, TId>
        where TEntity: DomainBase
    {
        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> entity);

        void Edit(TEntity entities);

        void Edit(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void Remove(IEnumerable<TEntity> entity);
    }
}
