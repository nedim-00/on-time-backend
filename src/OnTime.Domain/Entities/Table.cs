using System.ComponentModel.DataAnnotations;

namespace OnTime.Domain.Entities;

public class Table
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    public int RestaurantId { get; set; }
}
