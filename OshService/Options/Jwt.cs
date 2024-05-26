using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OshService.Options;

public class Jwt
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Key { get; init; }
    public required int ExpirationInMinutes { get; init; }
    public required bool RequireHttpsMetadata { get; init; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
