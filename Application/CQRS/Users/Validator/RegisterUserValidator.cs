using FluentValidation;
using FluentValidation.Validators;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.CQRS.Users.Validator;

public class RegisterUserValidator:AbstractValidator<Command>
{
    public RegisterUserValidator()
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
