using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Application.Common.Interfaces;
using Demo.Users.Domain.Entities;
using MediatR;

namespace Demo.Users.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUsersDbContext _usersDbContext;
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IUsersDbContext usersDbContext, IMediator mediator)
        {
            _usersDbContext = usersDbContext;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
                DOB = request.DOB,
                Active = true,
                CreatedDate = DateTime.Now
            };

            await _usersDbContext.Users.AddAsync(user);
            await _usersDbContext.SaveChangesAsync();

            await _mediator.Publish(new UserCreatedNotification(user.UserId));

            return user.UserId;
        }
    }
}
