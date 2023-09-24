using FluentValidation;

namespace OnTime.Application.Restaurants;

public class TableValidator : AbstractValidator<Table>
{
    public TableValidator()
    {
        _ = RuleFor(table => table.Capacity)
           .NotEmpty()
           .WithMessage("Number of seats is required.")
           .GreaterThan(0)
           .WithMessage("Number of seats must be greater than 0.");
    }
}
