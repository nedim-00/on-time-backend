using System.ComponentModel.DataAnnotations;

namespace OnTime.Domain.Entities;

public class Menu
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int RestaurantId { get; set; }

    [Required]
    public ICollection<MenuItem>? MenuItems { get; set; }
}
