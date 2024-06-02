using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using Microsoft.EntityFrameworkCore;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining;

[Service]
public class EmployeeResultTrainingService(
    EmployeeResultTrainingRepository repository,
    OshProgramAssignmentRepository assignmentRepository,
    TrainingQuestionRepository questionRepository,
    SecurityService service
)
{
    public Result<OshProgramResultStatusEnum> Result(long assigmentId, long trainingId,
        EmployeeResultTrainingViewCreate view)
    {
        var assigment = assignmentRepository.GetByEmployeeId(assigmentId, service.GetCurrentEmployeeId());
        if (assigment == null)
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.NoPrivilegesAvailable);
        }
        var training = questionRepository.Get()
            .Include(nameof(TrainingQuestionModel.Answers))
            .FirstOrDefault(e => e.Id == trainingId);

        if (training == null)
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.OshProgramNotFound);
        }
        if (!assigment.StartTraining.IsLast(assigment.OshProgram.TrainingMinutesDuration))
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.Timeout);
        }
        var entity = new EmployeeResultTrainingModel
        {
            TrainingQuestionId = trainingId,
            OshProgramAssignmentId = assigmentId,
            Answers = view.Answers.Select(e =>
                new EmployeeResultTrainingAnswerModel
                {
                    TrainingQuestionAnswerId = e,
                    Id = 0,
                    OshProgramStatusTrainingId = trainingId,
                    Training = null!,
                    ActualAnswer = null!
                }).ToList(),
            Id = 0,
            OshProgramAssignment = null!,
            TrainingQuestion = null!,
            Timestamp = DateTime.Now.ToUniversalTime()
        };
        repository.Create(entity);
        return new Result<OshProgramResultStatusEnum>(new { entity.Id, entity.Timestamp });
    }
}
