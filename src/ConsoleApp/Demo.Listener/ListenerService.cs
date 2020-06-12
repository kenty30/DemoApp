using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo.Listener
{
    public class ListenerService : IHostedService, IDisposable
    {
        ILogger<ListenerService> _logger;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public ListenerService(ILogger<ListenerService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting long running listner services.");

            if(_factory == null)
            {
                _factory = new ConnectionFactory() { HostName = "rabbitmq" };
                _connection = _factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(queue: "userCreated",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine("Received {0}", message);
                };

                _channel.BasicConsume(queue: "userCreated",
                                         autoAck: true,
                                         consumer: consumer);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping long running listner services.");
            return Task.CompletedTask;
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
