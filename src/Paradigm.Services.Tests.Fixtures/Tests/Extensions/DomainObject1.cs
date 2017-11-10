using Paradigm.Core.Mapping;
using Paradigm.Core.Mapping.Interfaces;
using Paradigm.ORM.Data.Attributes;
using Paradigm.Services.Domain;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    [Table]
    public class DomainObject1: DomainBase<IDomainObject1, DomainObject1>, IDomainObject1
    {
        [Column]
        public int Id { get; private set; }

        [Column]
        public string Name { get; private set; }

        public override bool IsNew() => this.Id == 0;

        public DomainObject1()
        {
            this.Id = 0;
            this.Name = string.Empty;
        }

        public static void RegisterMapping(IMapper mapper)
        {
            if (mapper.MapExists(typeof(IDomainObject1), typeof(DomainObject1)))
                return;

            mapper.Register<IDomainObject1, DomainObject1>();
        }

        public override DomainObject1 MapFrom(IDomainObject1 model)
        {
            return this.MapFrom(Mapper.Container, model);
        }

        public DomainObject1 MapFrom(IMapper mapper, IDomainObject1 model)
        {
            mapper.Map(model, this);
            return this;
        }
    }
}