using System;
using Paradigm.Services.Repositories.ORM;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Tests.Fixtures.Tests
{
    public class DomainObject1Repository: EditRepositoryBase<DomainObject1, int, IDomainObject1DatabaseAccess>, IDomainObject1Repository
    {
        public DomainObject1Repository(IServiceProvider serviceProvider, IDomainObject1DatabaseAccess databaseAccess, IUnitOfWork unitOfWork) : base(serviceProvider, databaseAccess, unitOfWork)
        {
        }
    }
}