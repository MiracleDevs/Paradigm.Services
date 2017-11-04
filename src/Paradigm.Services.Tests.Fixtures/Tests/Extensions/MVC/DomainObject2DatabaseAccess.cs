using System;
using Paradigm.ORM.Data.DatabaseAccess.Generic;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    internal class DomainObject2DatabaseAccess : DatabaseAccess<DomainObject2>, IDomainObject2DatabaseAccess
    {
        public DomainObject2DatabaseAccess(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}