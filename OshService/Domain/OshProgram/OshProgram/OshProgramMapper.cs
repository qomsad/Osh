using AutoMapper;

namespace OshService.Domain.OshProgram.OshProgram;

public class OshProgramMapper : Profile
{
    public OshProgramMapper()
    {
        CreateMap<OshProgramViewCreate, OshProgramModel>();
        CreateMap<OshProgramModel, OshProgramViewRead>();
    }
}
