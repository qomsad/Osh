using OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestion;

public class TrainingQuestionViewRead
{
    public required long Id { get; set; }

    public required int Index { get; set; }

    public TrainingQuestionType QuestionType { get; set; }

    public required string Question { get; set; }

    public required int Rate { get; set; }

    public required IEnumerable<TrainingQuestionAnswerViewRead> Answers { get; set; }
}
