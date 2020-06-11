namespace Demo.Users.Application.Common.Models
{
    public class RabbitMQAppSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
    }
}
