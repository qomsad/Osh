using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Domain.User.UserEmployee;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserEmployeeStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Пользователь с таким логином уже существует")]
    UserLoginExists,

    [Description("Специальность не найдена")]
    SpecialtyNotFound
}
