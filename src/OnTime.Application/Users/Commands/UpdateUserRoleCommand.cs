using OnTime.Domain.Enums;

namespace OnTime.Application.Users.Commands;

public class UpdateUserRoleCommand : IRequest<ApplicationResponse<UpdateUserResponseDto>>
{
    public int UserId { get; set; }

    public UserRole UserRole { get; set; }
}
