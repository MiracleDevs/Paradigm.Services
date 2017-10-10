using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Domain;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Repositories.EntityFramework
{
    public abstract partial class ReadRepositoryBase<TEntity, TId, TContext> : IReadRepository<TEntity, TId>
        where TEntity : DomainBase
        where TContext : DbContext
    {
        #region Properties

        protected IServiceProvider ServiceProvider { get; private set; }

        protected TContext Context { get; private set; }

        #endregion

        #region Constructor

        protected ReadRepositoryBase(IServiceProvider serviceProvider, TContext context, IUnitOfWork unitOfWork) 
        {
            this.ServiceProvider = serviceProvider;
            this.Context = context;
            unitOfWork?.RegisterRepository(this);
        }

        #endregion

        #region Implementation of IDisposable

        public virtual void Dispose()
        {
            this.Context?.Dispose();
            this.Context = null;
        }

        #endregion

        #region Implementation of IReadonlyRepository

        public abstract TEntity GetById(TId id);

        public virtual List<TEntity> GetAll()
        {
            return this.AsQueryable().ToList();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.AsQueryable().Count(predicate);
        }

        #endregion

        #region Protected Methods

        protected virtual IQueryable<TEntity> AsQueryable() => this.Context.Set<TEntity>();

        protected virtual DbSet<TEntity> GetDbSet() => this.Context.Set<TEntity>();

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