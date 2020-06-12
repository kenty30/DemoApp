using System;
using System.Text;
using Demo.Users.Application.Common.Interfaces;
using Demo.Users.Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Demo.Users.Infrastructure.Services
{
    public class RabbitMQService : IRabbitMQService, IDisposable
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitMQAppSettings _appSettings;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(IOptions<RabbitMQAppSettings> options, ILogger<RabbitMQService> logger)
        {
            _appSettings = options.Value;
            _logger = logger;

            try
            {
                _factory = new ConnectionFactory() { HostName = _appSettings.HostName, Port = _appSettings.Port };
                _factory.UserName = _appSettings.UserName;
                _factory.Password = _appSettings.Password;

                _connection = _factory.CreateConnection();

                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: options.Value.QueueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public bool Enqueue(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange: string.Empty,
                                    routingKey: _appSettings.QueueName,
                                    basicProperties: null,
                                    body: body);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return false;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }

            if (_channel != null)
            {
                _channel.Dispose();
            }
        }
    }
}
