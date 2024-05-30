using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Domain.Material.MaterialLearning.LearningSection;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LearningSectionStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Программа обучения не найдена")]
    OshProgramNotFound,
}
