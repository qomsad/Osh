using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Domain.OshProgram.OshProgram;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OshProgramStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,
}
