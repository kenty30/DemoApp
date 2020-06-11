using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Demo.Users.Infrastructure.Persistence;
using Newtonsoft.Json;

namespace Demo.Users.Api.IntegrationTests.Setup
{
    public static class Utilities
    {
        public static readonly Guid KnownUserID1 = new Guid("07bd0b03-b04a-458c-8053-ee4311002f66");
        public static readonly Guid KnownUserID2 = new Guid("4c4721b5-6410-430e-9c50-41b8b743ccb2");

        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }


        public static void InitializeDbForTests(UsersDbContext context)
        {
            context.Users.Add(new Domain.Entities.User
            {
                UserId = KnownUserID1,
                FirstName = "Bob",
                LastName = "Test",
                EmailAddress = "bob.test@test.com",
            });
            context.Users.Add(new Domain.Entities.User
            {
                UserId = KnownUserID2,
                FirstName = "Bob",
                LastName = "Test",
                EmailAddress = "bob.test@test.com",
            });
            context.SaveChanges();
        }
    }
}
