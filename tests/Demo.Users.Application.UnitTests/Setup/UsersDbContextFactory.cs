using System;
using Demo.Users.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Demo.Users.Application.UnitTests.Setup
{
    public static class UsersDbContextFactory
    {
        public static UsersDbContext Create()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var context = new UsersDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(UsersDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
