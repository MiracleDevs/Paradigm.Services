/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Domain;
using Paradigm.Services.Repositories.UOW;
using Paradigm.ORM.Data.DatabaseAccess.Generic;

namespace Paradigm.Services.Repositories.ORM
{
    public abstract partial class ReadRepositoryBase<TEntity, TId, TDatabaseAccess> : IReadRepository<TEntity, TId>
        where TEntity : DomainBase
        where TDatabaseAccess : IDatabaseAccess<TEntity>
    {
        #region Properties

        protected IServiceProvider ServiceProvider { get; }

        protected TDatabaseAccess DatabaseAccess { get; }

        protected IUnitOfWork UnitOfWork { get; }

        #endregion

        #region Constructor

        protected ReadRepositoryBase(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            this.DatabaseAccess = serviceProvider.GetService<TDatabaseAccess>();
            this.UnitOfWork = serviceProvider.GetService<IUnitOfWork>();
            this.UnitOfWork?.RegisterRepository(this);
        }


        protected ReadRepositoryBase(IServiceProvider serviceProvider, TDatabaseAccess databaseAccess)
        {
            this.ServiceProvider = serviceProvider;
            this.DatabaseAccess = databaseAccess;
            this.UnitOfWork = serviceProvider.GetService<IUnitOfWork>();
            this.UnitOfWork?.RegisterRepository(this);
        }

        protected ReadRepositoryBase(IServiceProvider serviceProvider, TDatabaseAccess databaseAccess, IUnitOfWork unitOfWork)
        {
            this.ServiceProvider = serviceProvider;
            this.DatabaseAccess = databaseAccess;
            this.UnitOfWork = unitOfWork;
            unitOfWork?.RegisterRepository(this);
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
            /* Database access are not disposable */
        }

        public virtual TEntity GetById(TId id)
        {
            return this.DatabaseAccess.SelectOne(id);
        }

        public virtual List<TEntity> GetAll()
        {
            return this.DatabaseAccess.Select();
        }

        #endregion

        #region Protected Methods

        protected TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            return this.ServiceProvider.GetService<TRepository>();
        }

        protected T Resolve<T>() where T : class
        {
            return this.ServiceProvider.GetService<T>();
        }

        #endregion
    }
}