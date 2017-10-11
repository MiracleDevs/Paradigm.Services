/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.Services.Repositories.ORM
{
    public abstract partial class EditRepositoryBase<TEntity, TId, TDatabaseAccess> 
    {
        public virtual async Task AddAsync(TEntity entity)
        {
            entity.BeforeAdd();
            await this.DatabaseAccess.InsertAsync(entity);
            entity.AfterAdd();
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities)
        {
            var entityList = entities as IList<TEntity> ?? entities.ToList();

            foreach (var entity in entityList)
                entity.BeforeAdd();

            await this.DatabaseAccess.InsertAsync(entityList);

            foreach (var entity in entityList)
                entity.AfterAdd();

        }

        public virtual async Task EditAsync(TEntity entity)
        {
            entity.BeforeEdit();
            await this.DatabaseAccess.UpdateAsync(entity);
            entity.AfterEdit();
        }

        public virtual async Task EditAsync(IEnumerable<TEntity> entities)
        {
            var entityList = entities as IList<TEntity> ?? entities.ToList();

            foreach (var entity in entityList)
                entity.BeforeEdit();

            await this.DatabaseAccess.UpdateAsync(entityList);

            foreach (var entity in entityList)
                entity.AfterEdit();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            entity.BeforeRemove();
            await this.DatabaseAccess.DeleteAsync(entity);
            entity.AfterRemove();
        }

        public virtual async Task RemoveAsync(IEnumerable<TEntity> entities)
        {
            var entityList = entities as IList<TEntity> ?? entities.ToList();

            foreach (var entity in entityList)
                entity.BeforeRemove();

            await this.DatabaseAccess.DeleteAsync(entityList);

            foreach (var entity in entityList)
                entity.AfterRemove();
        }
    }
}