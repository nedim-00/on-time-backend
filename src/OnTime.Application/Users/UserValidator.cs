using FluentValidation;
using OnTime.Application.Users.Commands;

namespace OnTime.Application.Users;

public class UserValidator : AbstractValidator<CreateUserCommand>
{
    public UserValidator()
    {
        _ = RuleFor(user => user.FirstName)
           .NotEmpty()
           .WithMessage("First name is required.");

        _ = RuleFor(user => user.LastName)
           .NotEmpty()
           .WithMessage("Last name is required.");

        _ = RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email domain.");

        _ = RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password should contain at least 8 characters.");

        _ = RuleFor(user => user.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm password is required.")
            .Equal(user => user.Password)
            .WithMessage("Password and confirm password do not match.");

        _ = RuleFor(user => user.UserRole)
            .NotEmpty()
            .WithMessage("User Role is required.")
            .IsInEnum()
            .WithMessage("You did not provide right User Role enum.");

        _ = RuleFor(user => user.UserStatus)
            .NotEmpty()
            .WithMessage("User Status is required.")
            .IsInEnum()
            .WithMessage("You did not provide right User Status enum.");
    }
}
