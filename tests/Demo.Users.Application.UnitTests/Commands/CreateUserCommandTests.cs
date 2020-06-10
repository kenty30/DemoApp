using System;
using System.Threading;
using Demo.Users.Application.Commands.Users.CreateUser;
using Demo.Users.Application.UnitTests.Setup;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Demo.Users.Application.UnitTests.Commands
{
    public class CreateUserCommandTests : CommandTestBase
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldCreateNewUserAndRaiseUserCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var sut = new CreateUserCommandHandler(DbContext, mediatorMock.Object);

            var command = new CreateUserCommand
            {
                FirstName = "Bob",
                LastName = "Test",
                EmailAddress = "bob.test@test.com",
                DOB = DateTime.Today.AddYears(-30)
            };

            // Act
            var result = sut.Handle(command, CancellationToken.None);

            // Assert
            mediatorMock.Verify(m =>
            m.Publish(It.Is<UserCreatedNotification>(cc =>
            cc.UserId == result.Result), It.IsAny<CancellationToken>()), Times.Once);

            result.Result
                .Should()
                .NotBeEmpty();
        }
    }
}
