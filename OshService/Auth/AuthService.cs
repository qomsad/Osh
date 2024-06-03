using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AspBoot.Configuration;
using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OshService.Domain.User.User;
using OshService.Options;
using OshService.Security;

namespace OshService.Auth;

[Service]
public class AuthService(
    UserRepository repository,
    IConfiguration configuration,
    IMapper mapper,
    SecurityService service)
{
    private readonly Jwt jwt = configuration.GetParam<Jwt>();

    public Result<AuthStatusEnum> Authenticate(AuthRequest request)
    {
        var user = repository.GetByLogin(request.Login);

        if (user == null)
        {
            return new Result<AuthStatusEnum>(AuthStatusEnum.UserNotFound);
        }

        if (!HashUtils.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new Result<AuthStatusEnum>(AuthStatusEnum.UserPasswordMismatch);
        }


        var response = new AuthResponse
        {
            JwtToken = GenerateToken(user.Login, user.Type.ToString()), User = mapper.Map<UserViewRead>(user)
        };

        return new Result<AuthStatusEnum>(response);
    }

    public string GenerateToken(string username, string role)
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

    public object? GetCurrentUser()
    {
        var user = service.GetCurrentUser();
        if (user != null)
        {
            return mapper.Map<UserViewRead>(user);
        }
        return null;
    }
}
