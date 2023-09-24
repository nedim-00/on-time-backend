namespace OnTime.Application.Users.Commands;

public class AuthenticateUserCommand : IRequest<ApplicationResponse<AuthenticationResponseDto>>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
