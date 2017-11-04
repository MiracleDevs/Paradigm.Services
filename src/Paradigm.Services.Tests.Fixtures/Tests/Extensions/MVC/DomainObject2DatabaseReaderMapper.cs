using Paradigm.ORM.Data.Database;
using Paradigm.ORM.Data.Mappers.Generic;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    internal class DomainObject2DatabaseReaderMapper : DatabaseReaderMapper<DomainObject2>, IDomainObject2DatabaseReaderMapper
    {
        public DomainObject2DatabaseReaderMapper(IDatabaseConnector connector) : base(connector)
        {
        }
    }
}