using AutoMapper;

namespace OshService.Domain.Specialty;

public class SpecialtyMapper : Profile
{
    public SpecialtyMapper()
    {
        CreateMap<SpecialtyViewCreate, SpecialtyModel>();
        CreateMap<SpecialtyModel, SpecialtyViewRead>();
    }
}
