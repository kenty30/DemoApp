using System.Collections.Generic;
using Demo.Users.Application.Common.Models;
using MediatR;

namespace Demo.Users.Application.Queries.Users.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {

    }
}
