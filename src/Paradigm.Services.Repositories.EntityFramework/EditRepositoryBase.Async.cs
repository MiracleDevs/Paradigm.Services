using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Repositories.EntityFramework
{
    public abstract partial class EditRepositoryBase<TEntity, TId, TContext>
    {
        #region Implementation of ICommiteable

        public virtual async Task CommitChangesAsync()
        {
            await this.Context.SaveChangesAsync();
        }

        #endregion

        #region Implementation of IEditRepository

        public virtual async Task AddAsync(TEntity entity)
        {
            await this.AddEntityAsync(entity);
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await this.AddEntityAsync(entity);
            }
        }

        public virtual Task EditAsync(TEntity entity)
        {
            this.EditEntity(entity);
            return Task.FromResult(default(object));
        }

        public virtual Task EditAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.EditEntity(entity);
            }

            return Task.FromResult(default(object));
        }

        public virtual Task RemoveAsync(TEntity entity)
        {
            this.RemoveEntity(entity);
            return Task.FromResult(default(object));
        }

        public virtual Task RemoveAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.RemoveEntity(entity);
            }

            return Task.FromResult(default(object));
        }

        #endregion

        #region Protected Methods

        protected virtual async Task AddEntityAsync<TOtherEntity>(TOtherEntity entity) where TOtherEntity : DomainBase
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.BeforeAdd();
            await this.Context.Set<TOtherEntity>().AddAsync(entity);
            entity.AfterAdd();
        }

        #endregion
    }
}