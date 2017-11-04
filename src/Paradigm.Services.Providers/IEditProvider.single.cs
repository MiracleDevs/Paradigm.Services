/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using Paradigm.Services.Domain;
using Paradigm.Services.Interfaces;

namespace Paradigm.Services.Providers
{
    public partial interface IEditProvider<TInterface, TDomain,in TId>: 
        IProvider
        where TInterface : IDomainInterface
        where TDomain : DomainBase<TInterface, TDomain>, TInterface
    {
        List<TDomain> FindEntity();

        TDomain GetEntity(TId id);

        TDomain Add(TInterface contract);

        IEnumerable<Tuple<TDomain, TInterface>> Add(IEnumerable<TInterface> contracts);

        TDomain Edit(TInterface contract, TId id);

        IEnumerable<Tuple<TDomain, TInterface>> Edit(IEnumerable<TInterface> contracts, Func<TInterface, TId> getByIdPredicate);

        void Remove(TId id);

        void Remove(IEnumerable<TId> ids);

        TDomain Save(TInterface contract, TId id);

        IEnumerable<Tuple<TDomain, TInterface>> Save(IEnumerable<TInterface> contracts, Func<TInterface, TId> getByIdPredicate);
    }
}