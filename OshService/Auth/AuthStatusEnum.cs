using System.ComponentModel;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace OshService.Auth;

[SwaggerSchema(Title = "Статусы аутентификации")]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AuthStatusEnum
{
    [Description("Пользователь не найден")]
    UserNotFound,

    [Description("Не верный пароль")]
    UserPasswordMismatch
}
