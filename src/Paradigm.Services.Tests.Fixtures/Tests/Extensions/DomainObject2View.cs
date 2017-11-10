using Paradigm.ORM.Data.Attributes;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    [Table]
    internal class DomainObject2View : DomainBase<IDomainObject2, DomainObject2View>, IDomainObject2
    {
        [Column]
        public int Id { get; }

        [Column]
        public string Name { get; }

        public override bool IsNew() => this.Id == 0;

        public DomainObject2View()
        {
            this.Id = 0;
            this.Name = string.Empty;
        }
    }
}