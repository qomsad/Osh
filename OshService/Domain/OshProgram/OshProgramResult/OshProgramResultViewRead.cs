namespace OshService.Domain.OshProgram.OshProgramResult;

public class OshProgramResultViewRead
{
    public required long Id { get; set; }

    public required decimal LearningResult { get; set; }

    public required decimal TrainingResult { get; set; }

    public required DateTime Timestamp { get; set; }
}
