using System;
using System.Threading.Tasks;
using Demo.Users.Application.Commands.Users.CreateUser;
using FluentAssertions;
using Xunit;

namespace Demo.Users.Application.UnitTests.Validators
{
    public class CreateUserCommandValidatorTests
    {
        [Fact]
        public async Task Validate_GivenValidRequest_ShouldReturnTrue()
        {
            // Arrange
            var sut = new CreateUserCommandValidator();
            var query = new CreateUserCommand
            {
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
            var expectedNumberOfValidationErrors = 4;
            var sut = new CreateUserCommandValidator();
            var query = new CreateUserCommand();

            // Act
            var res = await sut.ValidateAsync(query);

            // Assert
            res.IsValid.Should().BeFalse();
            res.Errors.Count.Equals(expectedNumberOfValidationErrors);
        }
    }
}
