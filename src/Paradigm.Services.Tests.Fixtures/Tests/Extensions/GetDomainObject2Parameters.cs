using Paradigm.ORM.Data.Attributes;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    [Routine("get_domain_object_2")]
    [StoredProcedureType("Reader")]
    [RoutineResult("ComponentView")]
    internal class GetDomainObject2Parameters
    {
        public string Name { get; set; }
    }
}