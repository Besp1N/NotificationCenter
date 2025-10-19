using FluentValidation;

namespace NotificationCenter.Application.User.Commands;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(100).WithMessage("LastName cannot be longer than 100.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required..")
            .EmailAddress().WithMessage("This in not valid email structure.")
            .MaximumLength(255).WithMessage("Email cannot be longer than 255.");
    }
}