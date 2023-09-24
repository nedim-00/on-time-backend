using FluentValidation;
using OnTime.Application.Users.Commands;

namespace OnTime.Application.Users;

public class UpdateUserValidator : AbstractValidator<UpdateUserRoleCommand>
{
    public UpdateUserValidator()
    {
        _ = RuleFor(user => user.UserId)
           .NotEmpty()
           .WithMessage("User id is required.");

        _ = RuleFor(user => user.UserRole)
            .NotEmpty()
            .WithMessage("User Role is required.")
            .IsInEnum()
            .WithMessage("You did not provide right User Role enum.");
    }
}
