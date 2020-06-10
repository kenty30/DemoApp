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
        public static readonly Guid KnownUserID = new Guid("07bd0b03-b04a-458c-8053-ee4311002f66");

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


            context.SaveChanges();
        }
    }
}
