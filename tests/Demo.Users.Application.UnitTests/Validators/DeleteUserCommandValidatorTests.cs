using System;
using System.Threading.Tasks;
using Demo.Users.Application.Commands.Users.DeleteUser;
using FluentAssertions;
using Xunit;

namespace Demo.Users.Application.UnitTests.Validators
{
    public class DeleteUserCommandValidatorTests
    {
        [Fact]
        public async Task Validate_GivenValidRequest_ShouldReturnTrue()
        {
            // Arrange
            var sut = new DeleteUserCommandValidator();
            var query = new DeleteUserCommand(Guid.NewGuid());

            // Act
            var res = await sut.ValidateAsync(query);

            // Assert
            res.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validate_GivenInvalidRequest_ShouldReturnFalseWithErrors()
        {
            // Arrange
            var expectedNumberOfValidationErrors = 1;
            var sut = new DeleteUserCommandValidator();
            var query = new DeleteUserCommand(Guid.Empty);

            // Act
            var res = await sut.ValidateAsync(query);

            // Assert
            res.IsValid.Should().BeFalse();
            res.Errors.Count.Equals(expectedNumberOfValidationErrors);
        }
    }
}
