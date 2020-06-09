using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Users.Application.Common.Exceptions;
using Demo.Users.Application.Common.Interfaces;
using Demo.Users.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Users.Application.Queries.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private const string NotFoundErrorMessage = "Requested user does not exists.";

        private readonly IUsersDbContext _usersDbContext;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUsersDbContext usersDbContext, IMapper mapper)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _usersDbContext.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(NotFoundErrorMessage);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
