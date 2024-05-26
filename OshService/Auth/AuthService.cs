using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AspBoot.Configuration;
using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using Microsoft.IdentityModel.Tokens;
using OshService.Domain.User;
using OshService.Options;

namespace OshService.Auth;

[Service]
public class AuthService(UserRepository repository, IConfiguration configuration)
{
    public Result<AuthResponse, AuthStatusEnum> Authenticate(AuthRequest request)
    {
        var user = repository.GetByLogin(request.Login);

        if (user == null)
        {
            return new Result<AuthResponse, AuthStatusEnum>(AuthStatusEnum.UserNotFound);
        }

        if (!HashUtils.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new Result<AuthResponse, AuthStatusEnum>(AuthStatusEnum.UserPasswordMismatch);
        }

        var response = new AuthResponse
        {
            Login = user.Login, JwtToken = GenerateToken(user.Login, "1", configuration.GetParam<Jwt>())
        };

        return new Result<AuthResponse, AuthStatusEnum>(response);
    }

    public static string GenerateToken(string username, string role, Jwt jwt)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        ];
        var token = new JwtSecurityToken
        (
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(jwt.ExpirationInMinutes)),
            signingCredentials: new SigningCredentials(jwt.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
