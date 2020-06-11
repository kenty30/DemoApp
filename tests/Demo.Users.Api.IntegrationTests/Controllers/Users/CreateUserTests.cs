using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Demo.Users.Api.IntegrationTests.Setup;
using Demo.Users.Application.Commands.Users.CreateUser;
using Xunit;

namespace Demo.Users.Api.IntegrationTests.Controllers.Users
{
    public class CreateUserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreateUserTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidRequest_ShouldCreateNewUser()
        {
            var client = _factory.GetAnonymousClient();
            var command = new CreateUserCommand
            {
                FirstName = "User 1",
                LastName = "Test",
                EmailAddress = "test.user@test.com",
                DOB = DateTime.Today.AddYears(-25)
            };

            var response = await client.PostAsync("/api/users", Utilities.GetRequestContent(command));

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldReturnBadRequestStatusCode()
        {
            var client = _factory.GetAnonymousClient();
            var command = new CreateUserCommand
            {
                FirstName = "User 1",
                LastName = "Test",
            };

            var response = await client.PostAsync("/api/users", Utilities.GetRequestContent(command));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
