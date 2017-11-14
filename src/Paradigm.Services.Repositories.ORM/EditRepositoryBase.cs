/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Paradigm.Services.Domain;
using Paradigm.Services.Repositories.UOW;
using Paradigm.ORM.Data.DatabaseAccess.Generic;

namespace Paradigm.Services.Repositories.ORM
{
    public abstract partial class EditRepositoryBase<TEntity, TId, TDatabaseAccess> : ReadRepositoryBase<TEntity, TId, TDatabaseAccess>, IEditRepository<TEntity, TId>
        where TEntity : DomainBase
        where TDatabaseAccess : IDatabaseAccess<TEntity>
    {
        #region Constructor

        protected EditRepositoryBase(IServiceProvider serviceProvider, TDatabaseAccess databaseAccess, IUnitOfWork unitOfWork) : base(serviceProvider, databaseAccess, unitOfWork)
        {
        }

        #endregion

        #region Public Methods

        public virtual void Add(TEntity entity)
        {
            entity.BeforeAdd(); 
            entity.BeforeSave();
            this.DatabaseAccess.Insert(entity);
            entity.AfterAdd();
            entity.AfterSave();
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            var entityList = entities as IList<TEntity> ?? entities.ToList();

            foreach (var entity in entityList)
            {
                entity.BeforeAdd();
                entity.BeforeSave();
            }

            this.DatabaseAccess.Insert(entityList);

            foreach (var entity in entityList)
            {
                entity.AfterAdd();
                entity.AfterSave();
            }
        }

        public virtual void Edit(TEntity entity)
        {
            entity.BeforeEdit();
            entity.BeforeSave();
            this.DatabaseAccess.Update(entity);
            entity.AfterEdit();
            entity.AfterSave();
        }

        public virtual void Edit(IEnumerable<TEntity> entities)
        {
            var entityList = entities as IList<TEntity> ?? entities.ToList();

            foreach (var entity in entityList)
            {
                entity.BeforeEdit();
                entity.BeforeSave();
            }

            this.DatabaseAccess.Update(entityList);

            foreach (var entity in entityList)
            {
                entity.AfterEdit();
                entity.AfterSave();
            }
        }

        public virtual void Remove(TEntity entity)
        {
            entity.BeforeRemove();
            this.DatabaseAccess.Delete(entity);
            entity.AfterRemove();
        }

        public virtual void Remove(IEnumerable<TEntity> entities)
        {
            var entityList = entities as IList<TEntity> ?? entities.ToList();

            foreach (var entity in entityList)
                entity.BeforeRemove();

            this.DatabaseAccess.Delete(entityList);

            foreach (var entity in entityList)
                entity.AfterRemove();
        }

        #endregion
    }
}