using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Setup.SuperUser;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SetupUserStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Пользователь с таким логином уже существует")]
    UserLoginExists
}
