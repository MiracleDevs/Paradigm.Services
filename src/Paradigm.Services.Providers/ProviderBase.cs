using System;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Services.Repositories;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Providers
{
    public abstract class ProviderBase
    {
        #region Properties

        protected IServiceProvider ServiceProvider { get; }

        protected IUnitOfWork UnitOfWork { get; }

        #endregion

        #region Constructor

        protected ProviderBase(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
        {
            this.ServiceProvider = serviceProvider;
            this.UnitOfWork = unitOfWork;
        }

        #endregion

        #region Protected Methods

        protected TRepository GetRepository<TRepository>() where TRepository: IRepository
        {
            return this.ServiceProvider.GetService<TRepository>();
        }

        protected TProvider GetProvider<TProvider>() where TProvider : IProvider
        {
            return this.ServiceProvider.GetService<TProvider>();
        }

        protected T Resolve<T>() where T : class
        {
            return this.ServiceProvider.GetService<T>();
        }

        #endregion
    }
}