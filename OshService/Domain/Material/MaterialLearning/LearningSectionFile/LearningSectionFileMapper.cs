using AutoMapper;

namespace OshService.Domain.Material.MaterialLearning.LearningSectionFile;

public class LearningSectionFileMapper : Profile
{
    public LearningSectionFileMapper()
    {
        CreateMap<LearningSectionFileViewCreate, LearningSectionFileModel>();
        CreateMap<LearningSectionFileModel, LearningSectionFileViewRead>();
    }
}
