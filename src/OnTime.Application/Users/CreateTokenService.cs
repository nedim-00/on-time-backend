using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace OnTime.Application.Users;

public class CreateTokenService : ICreateTokenService
{
    private readonly IConfiguration _configuration;

    public CreateTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, user.UserRole.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtSettings:Key"] ?? string.Empty));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(

            // "issuer",
            // "audience",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
