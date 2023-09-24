using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Domain.Entities;

public class User
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Email { get; set; }

    public string? Image { get; set; }

    public string? PhoneNumber { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public UserRole UserRole { get; set; }

    [Required]
    public UserStatus UserStatus { get; set; }

    [Required]
    public DateTimeOffset DateJoined { get; set; }
}
