using Demo.Users.Application.Common.Interfaces;

namespace Demo.Users.Api.IntegrationTests.Setup.FakeServices
{
    public class FakeRabbitMQService : IRabbitMQService
    {
        public bool Enqueue(string message)
        {
            return true;
        }
    }
}
