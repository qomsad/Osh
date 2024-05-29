using OshService.Domain.User.User;
using Swashbuckle.AspNetCore.Annotations;

namespace OshService.Auth;

[SwaggerSchema(Title = "Ответ аутентификации", Description = "Ответ аутентификации пользователя")]
public class AuthResponse
{
    [SwaggerSchema(Title = "JWT токен", Nullable = false, Format = "jwt-token")]
    public required string JwtToken { get; set; }

    [SwaggerSchema(Nullable = false)]
    public required UserViewRead User { get; set; }
}
