using System;
using System.Threading;
using System.Threading.Tasks;
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
            public UserCreatedNotificationHandler()
            {

            }

            public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
            {

            }
        }
    }
}
