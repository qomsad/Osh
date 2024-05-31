using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EmployeeProgramStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Программа обучения не найдена")]
    OshProgramNotFound,
}
