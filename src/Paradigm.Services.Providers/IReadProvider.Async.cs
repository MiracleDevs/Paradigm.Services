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