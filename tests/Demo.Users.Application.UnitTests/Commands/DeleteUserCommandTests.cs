using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Application.Commands.Users.DeleteUser;
using Demo.Users.Application.Common.Exceptions;
using Demo.Users.Application.UnitTests.Setup;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Demo.Users.Application.UnitTests.Commands
{
    public class DeleteUserCommandTests : CommandTestBase
    {
        private readonly DeleteUserCommandHandler _sut;

        public DeleteUserCommandTests()
        {
            _sut = new DeleteUserCommandHandler(DbContext);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRemoveUser()
        {
            // Arrange
            var validId = KnownUserID;
            var command = new DeleteUserCommand(validId);

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            var user = await DbContext.Users.FindAsync(validId);

            Assert.Null(user);
        }

        [Fact]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var validId = Guid.NewGuid();
            var command = new DeleteUserCommand(validId);

            // Act
            Func<Task<Unit>> result = () => _sut.Handle(command, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
