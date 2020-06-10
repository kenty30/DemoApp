using FluentValidation;

namespace Demo.Users.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.DOB)
                .NotEmpty();
        }
    }
}
