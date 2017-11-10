using Paradigm.ORM.Data.Attributes;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    [Table]
    internal class DomainObject2 : DomainBase<IDomainObject2, DomainObject2>, IDomainObject2
    {
        [Column]
        public int Id { get; }

        [Column]
        public string Name { get; }

        public override bool IsNew() => this.Id == 0;

        public DomainObject2()
        {
            this.Id = 0;
            this.Name = string.Empty;
        }
    }
}