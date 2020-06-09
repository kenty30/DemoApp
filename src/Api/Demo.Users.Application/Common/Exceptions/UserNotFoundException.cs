using System;

namespace Demo.Users.Application.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {

        }

        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}
