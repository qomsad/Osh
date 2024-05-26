using Swashbuckle.AspNetCore.Annotations;

namespace OshService.Auth;

[SwaggerSchema(Title = "Ответ аутентификации", Description = "Ответ аутентификации пользователя")]
public class AuthResponse
{
    [SwaggerSchema(Title = "Логин", Nullable = false)]
    public required string Login { get; set; }

    [SwaggerSchema(Title = "JWT токен", Nullable = false, Format = "jwt-token")]
    public required string JwtToken { get; set; }
}
