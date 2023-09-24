using OnTime.Domain.Enums;

namespace OnTime.Application.Users;

public record UserResponseDto
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Image { get; set; }

    public string? PhoneNumber { get; set; }

    public UserRole UserRole { get; set; }

    public UserStatus UserStatus { get; set; }

    public DateTimeOffset DateJoined { get; set; }
}
