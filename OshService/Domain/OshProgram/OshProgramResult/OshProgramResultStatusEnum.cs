using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Domain.OshProgram.OshProgramResult;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OshProgramResultStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Программа обучения не найдена")]
    OshProgramNotFound,

    [Description("Время вышло")]
    Timeout
}
