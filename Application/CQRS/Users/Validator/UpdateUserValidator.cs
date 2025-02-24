using FluentValidation;
using static Application.CQRS.Users.Handlers.Update;

namespace Application.CQRS.Users.Validator;

public class UpdateUserValidator : AbstractValidator<Command>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.Email).
             NotEmpty().
             MaximumLength(50).
             EmailAddress();

        RuleFor(u => u.Name).
             NotEmpty().
             MaximumLength(50);

        RuleFor(u => u.Surname).
             NotEmpty().
             MaximumLength(50);

        RuleFor(u => u.Phone).
             NotEmpty().
             MaximumLength(20).
             Matches(@"^\+994(5[015]|7[07])\d{7}$");
    }
}
