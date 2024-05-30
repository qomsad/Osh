using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Domain.Specialty;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SpecialtyStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Специальность с таким названием уже существует")]
    SpecialtyAlreadyExists,
}
