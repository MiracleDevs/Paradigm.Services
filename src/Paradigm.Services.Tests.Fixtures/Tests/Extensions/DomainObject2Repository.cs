using System;
using Paradigm.Services.Repositories.ORM;
using Paradigm.Services.Repositories.UOW;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions
{
    internal class DomainObject2Repository : EditRepositoryBase<DomainObject2, int, IDomainObject2DatabaseAccess>, IDomainObject2Repository
    {
        public DomainObject2Repository(IServiceProvider serviceProvider, IDomainObject2DatabaseAccess databaseAccess, IUnitOfWork unitOfWork) : base(serviceProvider, databaseAccess, unitOfWork)
        {
        }
    }
}