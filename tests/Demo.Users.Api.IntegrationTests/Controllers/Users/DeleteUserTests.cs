using System;
using System.Net;
using System.Threading.Tasks;
using Demo.Users.Api.IntegrationTests.Setup;
using Xunit;

namespace Demo.Users.Api.IntegrationTests.Controllers.Users
{
    public class DeleteUserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeleteUserTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidRequest_ShouldDeleteUser()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync($"/api/users/{Utilities.KnownUserID2}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldReturnBadRequestStatusCode()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync($"/api/users/{Guid.Empty}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidRequestWithNonExistingUserId_ShouldReturnNotFoundStatusCode()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync($"/api/users/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
