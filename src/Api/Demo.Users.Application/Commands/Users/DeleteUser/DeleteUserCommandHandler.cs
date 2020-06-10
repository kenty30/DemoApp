using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Application.Common.Constants;
using Demo.Users.Application.Common.Exceptions;
using Demo.Users.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Users.Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUsersDbContext _usersDbContext;

        public DeleteUserCommandHandler(IUsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (user == null)
            {
                throw new NotFoundException(ErrorMessages.UserNotFoundErrorMessage);
            }

            _usersDbContext.Users.Remove(user);
            await _usersDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
