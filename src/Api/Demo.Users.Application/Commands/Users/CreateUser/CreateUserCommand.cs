using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Demo.Users.Application.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string EmailAddress { get; set; }
    }
}
