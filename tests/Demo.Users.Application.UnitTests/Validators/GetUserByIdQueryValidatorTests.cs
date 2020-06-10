using System;
using System.Threading.Tasks;
using Demo.Users.Application.Queries.Users.GetUserById;
using FluentAssertions;
using Xunit;

namespace Demo.Users.Application.UnitTests.Validators
{
    public class GetUserByIdQueryValidatorTests
    {
        [Fact]
        public async Task Validate_GivenValidRequest_ShouldReturnTrue()
        {
            // Arrange
            var sut = new GetUserByIdQueryValidator();
            var query = new GetUserByIdQuery(Guid.NewGuid());

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
            var sut = new GetUserByIdQueryValidator();
            var query = new GetUserByIdQuery(Guid.Empty);

            // Act
            var res = await sut.ValidateAsync(query);

            // Assert
            res.IsValid.Should().BeFalse();
            res.Errors.Count.Equals(expectedNumberOfValidationErrors);
        }
    }
}
