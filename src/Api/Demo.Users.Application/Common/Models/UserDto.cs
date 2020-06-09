using System;

namespace Demo.Users.Application.Common.Models
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string EmailAddress { get; set; }
    }
}
