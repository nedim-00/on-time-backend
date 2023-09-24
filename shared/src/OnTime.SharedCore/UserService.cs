using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace OnTime.SharedCore;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

    public string UserId()
    {
        return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }

    public string Role()
    {
        return User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
    }

    public string Email()
    {
        return User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }
}
