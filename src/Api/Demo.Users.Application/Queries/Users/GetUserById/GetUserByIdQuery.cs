using System;
using Demo.Users.Application.Common.Models;
using MediatR;

namespace Demo.Users.Application.Queries.Users.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid UserId { get; set; }

        public GetUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
