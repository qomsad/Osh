using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestion;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TrainingQuestionStatusEnum
{
    [Description("Нет доступных привилегий")]
    NoPrivilegesAvailable,

    [Description("Программа обучения не найдена")]
    OshProgramNotFound,
}
