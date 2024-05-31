using AutoMapper;

namespace OshService.Domain.OshProgram.OshProgramResult;

public class OshProgramResultMapper : Profile
{
    public OshProgramResultMapper()
    {
        CreateMap<OshProgramResultModel, OshProgramResultViewRead>();
    }
}
