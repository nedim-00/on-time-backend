using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Application.Users.Commands;

public class CreateUserCommand : IRequest<ApplicationResponse<AuthenticationResponseDto>>
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public string? Image { get; set; }

    public string? PhoneNumber { get; set; }

    public required string Password { get; set; }

    public required string ConfirmPassword { get; set; }

    public UserRole UserRole { get; set; } = UserRole.User;

    public UserStatus UserStatus { get; set; } = UserStatus.Active;

    [DataType(DataType.Date)]
    public required DateTimeOffset DateJoined { get; set; }
}
