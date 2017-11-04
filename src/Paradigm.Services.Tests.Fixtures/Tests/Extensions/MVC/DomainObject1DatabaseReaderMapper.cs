using Paradigm.ORM.Data.Database;
using Paradigm.ORM.Data.Mappers.Generic;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    public class DomainObject1DatabaseReaderMapper: DatabaseReaderMapper<DomainObject1>, IDomainObject1DatabaseReaderMapper
    {
        public DomainObject1DatabaseReaderMapper(IDatabaseConnector connector) : base(connector)
        {
        }
    }
}