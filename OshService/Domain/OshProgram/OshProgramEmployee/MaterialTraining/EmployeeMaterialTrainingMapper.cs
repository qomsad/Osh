using AutoMapper;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;

namespace OshService.Domain.OshProgram.OshProgramEmployee.MaterialTraining;

public class EmployeeMaterialTrainingMapper : Profile
{
    public EmployeeMaterialTrainingMapper()
    {
        CreateMap<TrainingQuestionModel, EmployeeMaterialTrainingViewRead>();
        CreateMap<TrainingQuestionAnswerModel, EmployeeMaterialTrainingAnswerViewRead>();
    }
}
