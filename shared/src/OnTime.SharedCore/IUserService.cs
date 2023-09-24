using System.Security.Claims;

namespace OnTime.SharedCore;

public interface IUserService
{
    ClaimsPrincipal User { get; }

    string Email();

    string Role();
}
