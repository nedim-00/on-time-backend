using FluentValidation;
using OnTime.Application.Restaurants.Commands;

namespace OnTime.Application.Restaurants;

public class RestaurantValidator : AbstractValidator<CreateRestaurantCommand>
{
    public RestaurantValidator()
    {
        _ = RuleFor(restaurant => restaurant.Name)
           .NotEmpty()
           .WithMessage("Restaurant name is required.");

        _ = RuleFor(restaurant => restaurant.Description)
           .NotEmpty()
           .WithMessage("Description is required.");

        _ = RuleFor(restaurant => restaurant.Image)
           .NotEmpty()
           .WithMessage("Image is required.");

        _ = RuleFor(restaurant => restaurant.PhoneNumber)
           .NotEmpty()
           .WithMessage("Phone number is required.");

        _ = RuleFor(restaurant => restaurant.Address)
           .NotEmpty()
           .WithMessage("Address is required.");

        _ = RuleFor(restaurant => restaurant.City)
           .NotEmpty()
           .WithMessage("City is required.")
           .IsInEnum()
           .WithMessage("You did not provide right city enum.");

        _ = RuleFor(restaurant => restaurant.Municipality)
           .IsInEnum()
           .WithMessage("You did not provide right municipality enum.");

        _ = RuleFor(restaurant => restaurant.OpenTime)
           .NotEmpty()
           .WithMessage("Open time is required.");

        // .Matches("^([01][0-9]|2[0-3]):([0-5][0-9])$")
        // .WithMessage("You did not provide right open time format");
        _ = RuleFor(restaurant => restaurant.CloseTime)
           .NotEmpty()
           .WithMessage("Close time is required.");

        // .Matches("^([01][0-9]|2[0-3]):([0-5][0-9])$")
        // .WithMessage("You did not provide right close time format");
        _ = RuleFor(restaurant => restaurant.RestaurantStatus)
           .NotEmpty()
           .WithMessage("Restaurant status is required.")
           .IsInEnum()
           .WithMessage("You did not provide right restaurant status enum.");

        _ = RuleFor(restaurant => restaurant.RestaurantStatus)
           .NotEmpty()
           .WithMessage("Restaurant status is required.")
           .IsInEnum()
           .WithMessage("You did not provide right restaurant status enum.");

        _ = RuleForEach(restaurant => restaurant.Menus)
           .SetValidator(new MenuValidator());

        _ = RuleForEach(restaurant => restaurant.Tables)
           .SetValidator(new TableValidator());
    }
}
