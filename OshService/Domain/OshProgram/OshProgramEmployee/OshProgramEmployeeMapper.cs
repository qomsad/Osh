using AutoMapper;
using OshService.Domain.OshProgram.OshProgramAssignment;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

public class OshProgramEmployeeMapper : Profile
{
    public OshProgramEmployeeMapper()
    {
        CreateMap<OshProgramAssignmentModel, OshProgramEmployeeViewRead>();
    }
}
