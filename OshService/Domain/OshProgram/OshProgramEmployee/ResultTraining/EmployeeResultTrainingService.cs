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
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new
                Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.NoPrivilegesAvailable);
        }
        var assigment = assignmentRepository.Get()
            .Include(nameof(OshProgramAssignmentModel.OshProgram))
            .FirstOrDefault(
                e => e.UserEmployeeId == employee.Id
                     && e.Id == assigmentId
                     && e.Result == null
                     && e.StartTraining != null);
        if (assigment == null)
        {
            return new Result<OshProgramResultStatusEnum>
                (OshProgramResultStatusEnum.OshProgramNotFound);
        }
        if (assigment.StartTraining.IsLast(assigment.OshProgram.TrainingMinutesDuration))
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.Timeout);
        }

        var training = questionRepository.Get().Include(nameof(TrainingQuestionModel.Answers))
            .FirstOrDefault(e => e.Id == trainingId);

        if (training == null || training.Answers.Contains<>(view.Answers))
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.NoPrivilegesAvailable);
        }

        repository.Create(new EmployeeResultTrainingModel
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
                }),
            Id = 0,
            OshProgramAssignment = null!,
            TrainingQuestion = null!,
            Timestamp = DateTime.Now.ToUniversalTime()
        });

        return new Result<OshProgramResultStatusEnum>(new { });
    }
}
