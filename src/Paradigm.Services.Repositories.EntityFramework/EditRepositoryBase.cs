/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Paradigm.Services.Domain;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Repositories.EntityFramework
{
    public abstract partial class EditRepositoryBase<TEntity, TId, TContext> : ReadRepositoryBase<TEntity, TId, TContext>, IEditRepository<TEntity, TId>, ICommiteable
        where TEntity : DomainBase
        where TContext : DbContext
    {
        #region Constructor

        protected EditRepositoryBase(IServiceProvider serviceProvider, TContext context, IUnitOfWork unitOfWork) : base(serviceProvider, context, unitOfWork)
        {
        }

        #endregion

        #region Implementation of ICommiteable

        public void CommitChanges()
        {
            this.Context.SaveChanges();
        }

        #endregion

        #region Implementation of IEditRepository

        public virtual void Add(TEntity entity)
        {
            this.AddEntity(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                this.AddEntity(entity);
            }
        }

        public virtual void Edit(TEntity entity)
        {
            this.EditEntity(entity);
        }

        public void Edit(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                this.EditEntity(entity);
            }
        }

        public virtual void Remove(TEntity entity)
        {
            this.RemoveEntity(entity);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                this.RemoveEntity(entity);
            }
        }

        #endregion

        #region Protected Methods

        protected EntityEntry<TOtherEntity> GetEntityEntry<TOtherEntity>(TOtherEntity entity) where TOtherEntity: DomainBase
        {
            return this.Context.Entry(entity);
        }

        protected void AddEntity<TOtherEntity>(TOtherEntity entity) where TOtherEntity : DomainBase
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.BeforeAdd();
            entity.BeforeSave();
            this.Context.Set<TOtherEntity>().Add(entity);
            entity.AfterAdd();
            entity.AfterSave();
        }

        protected void EditEntity<TOtherEntity>(TOtherEntity entity) where TOtherEntity : DomainBase
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.BeforeEdit();       
            entity.BeforeSave();
            var entityEntry = this.GetEntityEntry(entity);

            if (entityEntry != null && entityEntry.State == EntityState.Detached)
            {
                entityEntry.State = EntityState.Modified;
                this.Context.Set<TOtherEntity>().Attach(entity);
            }

            entity.AfterEdit();
            entity.AfterSave();
        }

        protected void RemoveEntity<TOtherEntity>(TOtherEntity entity) where TOtherEntity : DomainBase
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.BeforeRemove();
            this.Context.Set<TOtherEntity>().Remove(entity);
            entity.AfterRemove();
        }

        protected void DetachEntity<TOtherEntity>(TOtherEntity entity) where TOtherEntity : DomainBase
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.GetEntityEntry(entity).State = EntityState.Detached;
        }

        #endregion
    }
}