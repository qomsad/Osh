using System.ComponentModel;

namespace OshService.Domain.OshProgram.OshProgramAssignment;

public enum OshProgramAssignmentStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Сотрудник не найден")]
    EmployeeNotFound,

    [Description("Программа обучения не найдена")]
    ProgramNotFound,
}
