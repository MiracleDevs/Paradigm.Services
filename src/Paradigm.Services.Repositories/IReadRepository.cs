using System.Collections.Generic;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Repositories
{
    public partial interface IReadRepository<TEntity, in TId>: IRepository 
        where TEntity: DomainBase
    {
        TEntity GetById(TId id);

        List<TEntity> GetAll();
    }
}
