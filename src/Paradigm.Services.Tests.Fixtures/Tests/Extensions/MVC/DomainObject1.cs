using Paradigm.ORM.Data.Attributes;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    [Table]
    public class DomainObject1: DomainBase<IDomainObject1, DomainObject1>, IDomainObject1
    {
        [Column]
        public int Id { get; }

        [Column]
        public string Name { get; }

        public override bool IsNew() => this.Id == 0;

        public DomainObject1()
        {
            this.Id = 0;
            this.Name = string.Empty;
        }
    }
}