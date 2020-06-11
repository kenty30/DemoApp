using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Users.Api.IntegrationTests.Setup;
using Demo.Users.Application.Common.Models;
using Xunit;

namespace Demo.Users.Api.IntegrationTests.Controllers.Users
{
    public class GetAllUsersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllUsersTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidRequest_ShouldReturnUsers()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync($"/api/users");

            response.EnsureSuccessStatusCode();

            var users = await Utilities.GetResponseContent<IEnumerable<UserDto>>(response);

            Assert.True(users.Any());
        }
    }
}
