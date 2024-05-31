using AutoMapper;
using OshService.Domain.OshProgram.OshProgramAssignment;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

public class EmployeeProgramMapper : Profile
{
    public EmployeeProgramMapper()
    {
        CreateMap<OshProgramAssignmentModel, EmployeeProgramViewRead>();
    }
}
