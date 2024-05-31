using OshService.Domain.OshProgram.OshProgram;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Domain.User.UserEmployee;

namespace OshService.Domain.OshProgram.OshProgramAssignment;

public class OshProgramAssignmentViewRead
{
    public required long Id { get; set; }

    public required UserEmployeeViewRead Employee { get; set; }

    public required OshProgramViewRead OshProgram { get; set; }

    public required DateTime AssignmentDate { get; set; }

    public required DateTime StartLearning { get; set; }

    public required DateTime StartTraining { get; set; }

    public OshProgramResultViewRead? Result { get; set; }
}
