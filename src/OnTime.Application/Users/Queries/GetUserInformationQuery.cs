using Microsoft.AspNetCore.Http;

namespace OnTime.Application.Users.Queries;

public class GetUserInformationQuery : IRequest<ApplicationResponse<UserResponseDto>>
{
    public HttpContext? HttpContext { get; set; }
}
