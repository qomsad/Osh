namespace OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;

public class TrainingQuestionAnswerViewRead
{
    public required long Id { get; set; }

    public required int Index { get; set; }

    public required string Value { get; set; }

    public required bool IsRight { get; set; }
}
