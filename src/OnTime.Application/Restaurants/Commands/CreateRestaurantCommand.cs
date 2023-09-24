using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Application.Restaurants.Commands;

public class CreateRestaurantCommand : IRequest<ApplicationResponse<RestaurantResponseDto>>
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Image { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Address { get; set; }

    public required City City { get; set; }

    public Municipality? Municipality { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public required TimeSpan OpenTime { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public required TimeSpan CloseTime { get; set; }

    public RestaurantStatus RestaurantStatus { get; set; } = RestaurantStatus.Active;

    public required int UserId { get; set; }

    public ICollection<Menu>? Menus { get; set; }

    public ICollection<Table>? Tables { get; set; }

    // public User? User { get; set; }
}
