using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Application.Common.Exceptions;
using Demo.Users.Application.Common.Models;
using Demo.Users.Application.Queries.Users.GetUserById;
using Demo.Users.Application.UnitTests.Setup;
using FluentAssertions;
using Xunit;

namespace Demo.Users.Application.UnitTests.Queries
{
    [Collection("QueryHandlerCollection")]
    public class GetUserByIdQueryTests
    {
        private readonly QueryHandlerTestFixture _fixture;

        public GetUserByIdQueryTests(QueryHandlerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnUser()
        {
            // Arrange
            var query = new GetUserByIdQuery(_fixture.KnownUserID);
            var queryHandler = new GetUserByIdQueryHandler(_fixture.DbContext, _fixture.Mapper);

            // Act
            UserDto user = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            user.Should().NotBeNull();
            user.UserId.Should().Equals(_fixture.KnownUserID);
        }

        [Fact]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetUserByIdQuery(Guid.NewGuid());
            var queryHandler = new GetUserByIdQueryHandler(_fixture.DbContext, _fixture.Mapper);

            // Act
            Func<Task<UserDto>> result = () => queryHandler.Handle(query, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
