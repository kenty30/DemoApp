using FluentValidation;

namespace Demo.Users.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
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
