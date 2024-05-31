using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using Microsoft.EntityFrameworkCore;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultLearning;

[Service]
public class EmployeeResultLearningService(
    EmployeeResultLearningRepository repository,
    OshProgramAssignmentRepository assignmentRepository,
    SecurityService service
)
{
    public Result<LearningSectionStatusEnum> Result(long assigmentId, long id)
    {
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new
                Result<LearningSectionStatusEnum>(LearningSectionStatusEnum.NoPrivilegesAvailable);
        }
        var assigment = assignmentRepository.Get()
            .Include(nameof(OshProgramAssignmentModel.OshProgram))
            .FirstOrDefault(
                e => e.UserEmployeeId == employee.Id
                     && e.Id == assigmentId
                     && e.Result == null
                     && e.StartTraining != null);
        if (assigment != null &&
            assigment.StartTraining.IsLast(assigment.OshProgram.TrainingMinutesDuration))
        {
            return new Result<LearningSectionStatusEnum>
                (LearningSectionStatusEnum.OshProgramNotFound);
        }
        repository.Create(new EmployeeResultLearningModel
        {
            LearningSectionId = id,
            OshProgramAssignmentId = assigmentId,
            Timestamp = DateTime.Now.ToUniversalTime(),
            Id = 0,
            LearningSection = null!,
            OshProgramAssignment = null!
        });
        return new Result<LearningSectionStatusEnum>((new { }));
    }
}
