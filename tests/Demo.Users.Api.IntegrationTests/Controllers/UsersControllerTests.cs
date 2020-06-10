using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Users.Api.IntegrationTests.Setup;
using Demo.Users.Application.Common.Models;
using Xunit;

namespace Demo.Users.Api.IntegrationTests.Controllers
{
    public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UsersControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateUser_GivenValidRequest_ReturnUserIdAndLocationHeader()
        {

        }

        [Fact]
        public async Task GetUser_GivenValidRequest_ReturnUser()
        {
            var client = _factory.GetAnonymousClient();

            var userId = Guid.NewGuid();

            var response = await client.GetAsync($"/api/users/{userId}");

            response.EnsureSuccessStatusCode();

            var user = await Utilities.GetResponseContent<UserDto>(response);

            Assert.Equal(userId, user.UserId);
        }
    }
}
