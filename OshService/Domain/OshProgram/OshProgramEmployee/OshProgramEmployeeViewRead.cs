using OshService.Domain.OshProgram.OshProgram;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

public class OshProgramEmployeeViewRead
{
    public required long Id { get; set; }

    public required OshProgramViewRead OshProgram { get; set; }

    public required DateTime AssignmentDate { get; set; }
}
