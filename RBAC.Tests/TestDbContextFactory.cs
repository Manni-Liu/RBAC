using System;
using Microsoft.EntityFrameworkCore;
using RBAC.Infrastructure.Persistence;

namespace RBAC.Tests.Common
{
   // Created a TestDbContextFactory to provide an in-memory database context for unit testing. 
   // Make application logic without relying on an actual database, making tests faster and more reliable.
    public static class TestDbContextFactory
    {
        public static RbacDbContext Create()
        {
            var options = new DbContextOptionsBuilder<RbacDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new RbacDbContext(options);

            return context;
        }
    }
}

