using System;
using Paradigm.ORM.Data.DatabaseAccess.Generic;

namespace Paradigm.Services.Tests.Fixtures.Tests.Extensions.MVC
{
    public class DomainObject1DatabaseAccess: DatabaseAccess<DomainObject1>, IDomainObject1DatabaseAccess
    {
        public DomainObject1DatabaseAccess(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}