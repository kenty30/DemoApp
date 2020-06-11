using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Application.Common.Interfaces;
using MediatR;

namespace Demo.Users.Application.Commands.Users.CreateUser
{
    public class UserCreatedNotification : INotification
    {
        public Guid UserId { get; set; }

        public UserCreatedNotification(Guid userID)
        {
            UserId = userID;
        }

        public class UserCreatedNotificationHandler : INotificationHandler<UserCreatedNotification>
        {
            private readonly IRabbitMQService _rabbitMQService;

            public UserCreatedNotificationHandler(IRabbitMQService rabbitMQService)
            {
                _rabbitMQService = rabbitMQService;
            }

            public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
            {
                return Task.FromResult(_rabbitMQService.Enqueue($"UserCreated: {notification.UserId}"));
            }
        }
    }
}
