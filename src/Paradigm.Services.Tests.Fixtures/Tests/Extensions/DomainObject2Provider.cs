using System;
using Paradigm.Services.Providers;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    internal class DomainObject2Provider : EditProviderBase<IDomainObject2, DomainObject2, IDomainObject2Repository, int>, IDomainObject2Provider
    {
        public DomainObject2Provider(IServiceProvider serviceProvider, IUnitOfWork unitOfWork) : base(serviceProvider, unitOfWork)
        {
        }
    }
}