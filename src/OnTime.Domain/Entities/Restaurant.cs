using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Domain.Entities;

public class Restaurant
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Image { get; set; }

    [Required]
    public string? PhoneNumber { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public City City { get; set; }

    [Required]
    public Municipality Municipality { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public TimeSpan OpenTime { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public TimeSpan CloseTime { get; set; }

    [Required]
    public RestaurantStatus RestaurantStatus { get; set; }

    [Required]
    public int UserId { get; set; }

    public ICollection<Menu>? Menus { get; set; }

    public ICollection<Table>? Tables { get; set; }

    public User? User { get; set; }
}
