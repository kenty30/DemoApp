using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Application.Common.Constants;
using Demo.Users.Application.Common.Exceptions;
using Demo.Users.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Users.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUsersDbContext _usersDbContext;

        public UpdateUserCommandHandler(IUsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (user == null)
            {
                throw new NotFoundException(ErrorMessages.UserNotFoundErrorMessage);
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.EmailAddress = request.EmailAddress;
            user.DOB = request.DOB;

            await _usersDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
