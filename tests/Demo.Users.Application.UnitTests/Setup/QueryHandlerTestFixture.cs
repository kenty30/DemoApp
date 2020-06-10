using System;
using AutoMapper;
using Demo.Users.Application.Common.Mappings;
using Demo.Users.Infrastructure.Persistence;
using Xunit;

namespace Demo.Users.Application.UnitTests.Setup
{
    public class QueryHandlerTestFixture : IDisposable
    {
        public readonly Guid KnownUserID = new Guid("07bd0b03-b04a-458c-8053-ee4311002f66");
        public UsersDbContext DbContext { get; }
        public IMapper Mapper { get; private set; }

        public QueryHandlerTestFixture()
        {
            DbContext = UsersDbContextFactory.Create();

            PopulateTestData();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void PopulateTestData()
        {
            DbContext.Users.Add(new Domain.Entities.User
            {
                UserId = KnownUserID,
                FirstName = "Bob",
                LastName = "Test",
                EmailAddress = "bob.test@test.com",
            });
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UsersDbContextFactory.Destroy(DbContext);
            }
        }
    }

    [CollectionDefinition("QueryHandlerCollection")]
    public class QueryHandlerCollection : ICollectionFixture<QueryHandlerTestFixture> { }
}
