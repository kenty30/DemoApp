using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Application.Commands.Users.UpdateUser;
using Demo.Users.Application.Common.Exceptions;
using Demo.Users.Application.UnitTests.Setup;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Demo.Users.Application.UnitTests.Commands
{
    public class UpdateUserCommandTests : CommandTestBase
    {
        private readonly UpdateUserCommandHandler _sut;

        public UpdateUserCommandTests()
        {
            _sut = new UpdateUserCommandHandler(DbContext);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldUpdateUser()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                UserId = KnownUserID,
                FirstName = "Bob",
                LastName = "Test",
                EmailAddress = "updated.email@test.com",
                DOB = DateTime.Today.AddYears(-30)
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            var user = await DbContext.Users.FindAsync(KnownUserID);
            Assert.Equal(command.EmailAddress, user.EmailAddress);
        }

        [Fact]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                UserId = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Test",
                EmailAddress = "updated.email@test.com",
                DOB = DateTime.Today.AddYears(-30)
            };

            // Act
            Func<Task<Unit>> result = () => _sut.Handle(command, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
