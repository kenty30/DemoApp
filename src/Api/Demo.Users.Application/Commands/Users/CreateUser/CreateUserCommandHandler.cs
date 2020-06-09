using System;
using System.Collections.Generic;
using System.Text;
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

        public CreateUserCommandHandler(IUsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
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

            return user.UserId;
        }
    }
}
