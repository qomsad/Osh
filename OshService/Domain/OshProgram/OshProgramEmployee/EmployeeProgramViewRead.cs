using OshService.Domain.OshProgram.OshProgram;
using OshService.Domain.OshProgram.OshProgramResult;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

public class EmployeeProgramViewRead
{
    public required long Id { get; set; }

    public required long OshProgramId { get; set; }

    public required OshProgramViewRead OshProgram { get; set; }

    public required DateTime AssignmentDate { get; set; }

    public DateTime? StartLearning { get; set; }

    public DateTime? StartTraining { get; set; }

    public long? OshProgramResultId { get; set; }

    public OshProgramResultViewRead? Result { get; set; }
}
