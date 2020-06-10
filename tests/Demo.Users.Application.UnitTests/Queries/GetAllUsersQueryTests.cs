using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Application.Common.Models;
using Demo.Users.Application.Queries.Users.GetAllUsers;
using Demo.Users.Application.UnitTests.Setup;
using Xunit;

namespace Demo.Users.Application.UnitTests.Queries
{
    [Collection("QueryHandlerCollection")]
    public class GetAllUsersQueryTests
    {
        private readonly QueryHandlerTestFixture _fixture;

        public GetAllUsersQueryTests(QueryHandlerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnUser()
        {
            // Arrange
            var query = new GetAllUsersQuery();
            var queryHandler = new GetAllUsersQueryHandler(_fixture.DbContext, _fixture.Mapper);

            // Act
            IEnumerable<UserDto> users = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(users.Any());
        }
    }
}
