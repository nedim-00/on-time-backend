using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Application.Restaurants.Commands;

public class UpdateRestaurantCommand : IRequest<ApplicationResponse<RestaurantResponseDto>>
{
    public int Id { get; set; }

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
}
