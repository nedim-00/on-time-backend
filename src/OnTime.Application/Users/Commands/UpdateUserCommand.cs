namespace OnTime.Application.Users.Commands;

public class UpdateUserCommand : IRequest<ApplicationResponse<UserResponseDto>>
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Image { get; set; }
}
