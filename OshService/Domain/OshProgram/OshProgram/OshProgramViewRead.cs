using OshService.Domain.Specialty;

namespace OshService.Domain.OshProgram.OshProgram;

public class OshProgramViewRead
{
    public required long Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required int LearningMinutesDuration { get; set; }

    public required int TrainingMinutesDuration { get; set; }

    public SpecialtyViewRead? Specialty { get; set; }

    public required OshProgramAutoAssignment AutoAssignmentType { get; set; }

    public int? MaxAutoAssignments { get; set; }

    public required int TrainingSuccessRate { get; set; }
}
