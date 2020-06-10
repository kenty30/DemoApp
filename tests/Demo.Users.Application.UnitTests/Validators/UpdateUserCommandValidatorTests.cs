using System;
using System.Threading.Tasks;
using Demo.Users.Application.Commands.Users.UpdateUser;
using Demo.Users.Application.UnitTests.Setup;
using FluentAssertions;
using Xunit;

namespace Demo.Users.Application.UnitTests.Validators
{
    public class UpdateUserCommandValidatorTests : CommandTestBase
    {
        [Fact]
        public async Task Validate_GivenValidRequest_ShouldReturnTrue()
        {
            // Arrange
            var sut = new UpdateUserCommandValidator();
            var query = new UpdateUserCommand
            {
                UserId = KnownUserID,
                FirstName = "Bob",
                LastName = "Test",
                EmailAddress = "test@test.com",
                DOB = DateTime.Today.AddYears(-30)
            };

            // Act
            var res = await sut.ValidateAsync(query);

            // Assert
            res.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validate_GivenInvalidRequest_ShouldReturnFalseWithErrors()
        {
            // Arrange
            var expectedNumberOfValidationErrors = 5;
            var sut = new UpdateUserCommandValidator();
            var query = new UpdateUserCommand();

            // Act
            var res = await sut.ValidateAsync(query);

            // Assert
            res.IsValid.Should().BeFalse();
            res.Errors.Count.Equals(expectedNumberOfValidationErrors);
        }
    }
}
