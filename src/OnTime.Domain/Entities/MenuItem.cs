using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnTime.Domain.Enums;

namespace OnTime.Domain.Entities;

public class MenuItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(9, 2)")]
    public decimal Price { get; set; }

    [Required]
    public string? Image { get; set; }

    [Required]
    public MenuItemCategory Category { get; set; }

    [Required]
    public int MenuId { get; set; }
}
