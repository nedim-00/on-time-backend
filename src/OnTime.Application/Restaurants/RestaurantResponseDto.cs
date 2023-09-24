using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Application.Restaurants;

public record RestaurantResponseDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Image { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Address { get; set; }

    public required City City { get; set; }

    public required Municipality? Municipality { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public required TimeSpan OpenTime { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public required TimeSpan CloseTime { get; set; }

    public required RestaurantStatus RestaurantStatus { get; set; }

    public ICollection<Menu>? Menus { get; set; }

    public ICollection<Table>? Tables { get; set; }
}
