using OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestion;

public class TrainingQuestionViewCreate
{
    public TrainingQuestionType QuestionType { get; set; }

    public required string Question { get; set; }

    public required int Rate { get; set; }

    public required IEnumerable<TrainingQuestionAnswerViewCreate> Answers { get; set; }
}
