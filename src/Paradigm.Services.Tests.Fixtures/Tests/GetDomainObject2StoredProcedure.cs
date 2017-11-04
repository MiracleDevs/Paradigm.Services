using System;
using Paradigm.ORM.Data.StoredProcedures;

namespace Paradigm.Services.Tests.Fixtures.Tests
{
    internal class GetDomainObject2StoredProcedure : ReaderStoredProcedure<GetDomainObject2Parameters, DomainObject2>, IGetDomainObject2StoredProcedure
    {
        public GetDomainObject2StoredProcedure(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}