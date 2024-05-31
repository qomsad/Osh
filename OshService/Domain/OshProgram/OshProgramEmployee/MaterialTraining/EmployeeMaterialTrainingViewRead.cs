using OshService.Domain.Material.MaterialTraining.TrainingQuestion;

namespace OshService.Domain.OshProgram.OshProgramEmployee.MaterialTraining;

public class EmployeeMaterialTrainingViewRead
{
    public required long Id { get; set; }

    public TrainingQuestionType QuestionType { get; set; }

    public required string Question { get; set; }

    public required IEnumerable<EmployeeMaterialTrainingAnswerViewRead> Answers { get; set; }
}
