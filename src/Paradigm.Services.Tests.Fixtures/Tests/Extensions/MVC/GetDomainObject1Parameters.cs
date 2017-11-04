using Paradigm.ORM.Data.Attributes;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    [Routine("get_domain_object_1")]
    [StoredProcedureType("Reader")]
    [RoutineResult("ComponentView")]
    public class GetDomainObject1Parameters
    {
        public string Name { get; set; }
    }
}