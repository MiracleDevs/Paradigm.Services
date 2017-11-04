using System;
using Paradigm.ORM.Data.StoredProcedures;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    public class GetDomainObject1StoredProcedure: ReaderStoredProcedure<GetDomainObject1Parameters, DomainObject1>, IGetDomainObject1StoredProcedure
    {
        public GetDomainObject1StoredProcedure(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}