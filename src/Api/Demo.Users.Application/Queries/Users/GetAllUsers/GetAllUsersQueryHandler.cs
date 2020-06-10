using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Demo.Users.Application.Common.Interfaces;
using Demo.Users.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Users.Application.Queries.Users.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUsersDbContext _usersDbContext;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUsersDbContext usersDbContext, IMapper mapper)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            // TODO: Add paging
            return await _usersDbContext.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
