using AutoMapper;

namespace OshService.Domain.OshProgram.OshProgramAssignment;

public class OshProgramAssignmentMapper : Profile
{
    public OshProgramAssignmentMapper()
    {
        CreateMap<OshProgramAssignmentViewCreate, OshProgramAssignmentModel>();
        CreateMap<OshProgramAssignmentModel, OshProgramAssignmentViewRead>();
    }
}
