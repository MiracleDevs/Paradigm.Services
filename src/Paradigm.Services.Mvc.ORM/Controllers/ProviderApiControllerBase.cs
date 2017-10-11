/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using Paradigm.Services.Exceptions;
using Paradigm.Services.Providers;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Mvc.ORM.Controllers
{
    public abstract class ProviderApiControllerBase<TProvider>: ApiControllerBase
        where TProvider: IProvider
    {
        #region Properties

        protected TProvider Provider { get; }

        #endregion

        #region Constructor

        protected ProviderApiControllerBase(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IExceptionHandler exceptionHandler, TProvider provider) : base(serviceProvider, unitOfWork, exceptionHandler)
        {
            this.Provider = provider;
        }

        #endregion
    }
}