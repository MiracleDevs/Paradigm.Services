/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paradigm.Services.Providers
{
    public partial interface IEditProvider<TInterface, TDomain, TView, in TId>
    {
        Task<List<TDomain>> FindEntityAsync();

        Task<TDomain> GetEntityAsync(TId id);

        Task<TDomain> AddAsync(TInterface contract);

        Task<IEnumerable<Tuple<TDomain, TInterface>>> AddAsync(IEnumerable<TInterface> contracts);

        Task<TDomain> EditAsync(TInterface contract, TId id);

        Task<IEnumerable<Tuple<TDomain, TInterface>>> EditAsync(IEnumerable<TInterface> contracts, Func<TInterface, TId> getByIdPredicate);

        Task RemoveAsync(TId id);

        Task RemoveAsync(IEnumerable<TId> ids);

        Task<TDomain> SaveAsync(TInterface contract, TId id);

        Task<TView> SaveAsync(TInterface contract, Func<TInterface, TId> getByIdPredicate);

        Task<IEnumerable<Tuple<TDomain, TInterface>>> SaveAsync(IEnumerable<TInterface> contracts, Func<TInterface, TId> getByIdPredicate);
    }
}