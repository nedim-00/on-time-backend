using FluentValidation;

namespace OnTime.Application.Restaurants;

public class MenuItemsValidator : AbstractValidator<MenuItem>
{
    public MenuItemsValidator()
    {
        _ = RuleFor(menuItem => menuItem.Name)
           .NotEmpty()
           .WithMessage("Menu item name is required.");

        _ = RuleFor(menuItem => menuItem.Price)
           .NotEmpty()
           .WithMessage("Price is required.")
           .GreaterThan(0)
           .WithMessage("Price must be greater than 0.");
    }
}
