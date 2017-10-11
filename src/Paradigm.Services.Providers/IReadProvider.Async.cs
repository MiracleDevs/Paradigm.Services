/*!
* Paradigm Framework - Service Libraries
* Copyright(c) 2017 Miracle Devs, Inc
* Licensed under MIT(https://github.com/MiracleDevs/Paradigm.Services/blob/master/LICENSE)
*/

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paradigm.Services.Providers
{
    public partial interface IReadProvider<TView, in TId>
    {
        Task<List<TView>> FindViewAsync();

        Task<TView> GetViewAsync(TId id);
    }
}