namespace OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;

public class TrainingQuestionAnswerViewCreate
{
    public required int Index { get; set; }

    public required string Value { get; set; }

    public required bool IsRight { get; set; }
}
