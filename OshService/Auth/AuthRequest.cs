using Swashbuckle.AspNetCore.Annotations;

namespace OshService.Auth;

[SwaggerSchema(Title = "Запрос аутентификации", Description = "Запрос аутентификации пользователя")]
public class AuthRequest
{
    [SwaggerSchema(Title = "Логин", Nullable = false)]
    public required string Login { get; set; }

    [SwaggerSchema(Title = "Пароль", Nullable = false)]
    public required string Password { get; set; }
}
