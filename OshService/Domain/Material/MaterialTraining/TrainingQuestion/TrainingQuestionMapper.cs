using AutoMapper;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestion;

public class TrainingQuestionMapper : Profile
{
    public TrainingQuestionMapper()
    {
        CreateMap<TrainingQuestionViewCreate, TrainingQuestionModel>();
        CreateMap<TrainingQuestionModel, TrainingQuestionViewRead>();
    }
}
