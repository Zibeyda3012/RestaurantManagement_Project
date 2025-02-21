using Application.CQRS.Categories.Command.Requests;
using FluentValidation;

namespace Application.CQRS.Categories.Validator;

public class CreateCategoryValidator:AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
    }
}
