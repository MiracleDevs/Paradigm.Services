using Paradigm.ORM.Data.Attributes;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    [Table]
    public class DomainObject1View: DomainBase<IDomainObject1, DomainObject1View>, IDomainObject1
    {
        [Column]
        public int Id { get; }

        [Column]
        public string Name { get; }

        public override bool IsNew() => this.Id == 0;

        public DomainObject1View()
        {
            this.Id = 0;
            this.Name = string.Empty;
        }
    }
}