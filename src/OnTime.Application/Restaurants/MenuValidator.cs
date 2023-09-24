using FluentValidation;

namespace OnTime.Application.Restaurants;

public class MenuValidator : AbstractValidator<Menu>
{
    public MenuValidator()
    {
        _ = RuleFor(menu => menu.Name)
           .NotEmpty()
           .WithMessage("Menu name is required.");

        _ = RuleForEach(menu => menu.MenuItems)
           .SetValidator(new MenuItemsValidator());
    }
}
