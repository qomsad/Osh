using AutoMapper;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;

public class TrainingQuestionAnswerMapper : Profile
{
    public TrainingQuestionAnswerMapper()
    {
        CreateMap<TrainingQuestionAnswerViewCreate, TrainingQuestionAnswerModel>();
        CreateMap<TrainingQuestionAnswerModel, TrainingQuestionAnswerViewRead>();
    }
}
