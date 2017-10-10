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