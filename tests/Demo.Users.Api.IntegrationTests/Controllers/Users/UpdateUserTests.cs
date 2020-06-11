using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Demo.Users.Api.IntegrationTests.Setup;
using Demo.Users.Application.Commands.Users.UpdateUser;
using Xunit;

namespace Demo.Users.Api.IntegrationTests.Controllers.Users
{
    public class UpdateUserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UpdateUserTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidRequest_ShouldUpdateUser()
        {
            var client = _factory.GetAnonymousClient();
            var command = new UpdateUserCommand
            {
                UserId = Utilities.KnownUserID1,
                FirstName = "User 1",
                LastName = "Test",
                EmailAddress = "user.test1@test.com",
                DOB = DateTime.Today.AddYears(-25)
            };

            HttpContent content = Utilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/users/{Utilities.KnownUserID1}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenValidRequestWithNonExistingUserId_ShouldReturnNotFoundStatusCode()
        {
            var client = _factory.GetAnonymousClient();
            var command = new UpdateUserCommand
            {
                FirstName = "User 1",
                LastName = "Test",
                EmailAddress = "user.test1@test.com",
                DOB = DateTime.Today.AddYears(-25)
            };

            var response = await client.PutAsync($"/api/users/{Guid.NewGuid()}", Utilities.GetRequestContent(command));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldReturnBadRequestStatusCode()
        {
            var client = _factory.GetAnonymousClient();
            var command = new UpdateUserCommand
            {
                FirstName = "User 1",
                LastName = "Test",
            };

            var response = await client.PutAsync($"/api/users/{Utilities.KnownUserID1}", Utilities.GetRequestContent(command));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
