using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Users.Api.IntegrationTests.Setup;
using Demo.Users.Application.Common.Models;
using Xunit;

namespace Demo.Users.Api.IntegrationTests.Controllers.Users
{
    public class GetUserByIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetUserByIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidRequest_ReturnUser()
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
