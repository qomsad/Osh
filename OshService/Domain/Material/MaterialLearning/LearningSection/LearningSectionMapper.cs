using AutoMapper;

namespace OshService.Domain.Material.MaterialLearning.LearningSection;

public class LearningSectionMapper : Profile
{
    public LearningSectionMapper()
    {
        CreateMap<LearningSectionViewCreate, LearningSectionModel>();
        CreateMap<LearningSectionModel, LearningSectionViewRead>();
    }
}
