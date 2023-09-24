namespace OnTime.Application.Users;

public interface ICreateTokenService
{
    public string CreateToken(User user);
}
