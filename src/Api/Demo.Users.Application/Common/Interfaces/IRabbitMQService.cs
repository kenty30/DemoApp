namespace Demo.Users.Application.Common.Interfaces
{
    public interface IRabbitMQService
    {
        bool Enqueue(string message);
    }
}
