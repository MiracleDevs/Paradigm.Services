/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Collections.Generic;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Providers
{
    public partial interface IReadProvider<TView, in TId>: IProvider
        where TView : DomainBase
    {
        List<TView> FindView();

        TView GetView(TId id);
    }
}