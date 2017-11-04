using System;
using Paradigm.Services.Providers;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    public class DomainObject1Provider: EditProviderBase<IDomainObject1, DomainObject1, IDomainObject1Repository, int>, IDomainObject1Provider
    {
        public DomainObject1Provider(IServiceProvider serviceProvider, IUnitOfWork unitOfWork) : base(serviceProvider, unitOfWork)
        {
        }
    }
}