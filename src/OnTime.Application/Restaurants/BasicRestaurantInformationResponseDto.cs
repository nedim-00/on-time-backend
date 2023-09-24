using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Application.Restaurants;

public record BasicRestaurantInformationResponseDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public City City { get; set; }

    public Municipality? Municipality { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public required TimeSpan OpenTime { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public required TimeSpan CloseTime { get; set; }

    public RestaurantStatus RestaurantStatus { get; set; }
}
