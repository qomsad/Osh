using AspBoot.Handler;
using AspBoot.Service;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultLearning;

[Service]
public class EmployeeResultLearningService(
    EmployeeResultLearningRepository repository,
    OshProgramAssignmentRepository assignmentRepository,
    LearningSectionRepository learningSectionRepository,
    SecurityService service
)
{
    public Result<LearningSectionStatusEnum> Result(long assigmentId, long questionId)
    {
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new
                Result<LearningSectionStatusEnum>(LearningSectionStatusEnum.NoPrivilegesAvailable);
        }
        var assigment = assignmentRepository.Get()
            .FirstOrDefault(
                e => e.UserEmployeeId == employee.Id
                     && e.Id == assigmentId
                     && e.Result == null
                     && e.StartLearning != null
                     && e.StartTraining == null);
        if (assigment == null)
        {
            return new Result<LearningSectionStatusEnum>
                (LearningSectionStatusEnum.OshProgramNotFound);
        }
        var question = learningSectionRepository.Get().FirstOrDefault(e => e.Id == questionId);
        if (question == null)
        {
            return new Result<LearningSectionStatusEnum>(LearningSectionStatusEnum.NoPrivilegesAvailable);
        }
        repository.Create(new EmployeeResultLearningModel
        {
            LearningSectionId = question.Id,
            OshProgramAssignmentId = assigmentId,
            Timestamp = DateTime.Now.ToUniversalTime(),
            Id = 0,
            LearningSection = null!,
            OshProgramAssignment = null!
        });
        return new Result<LearningSectionStatusEnum>((new { }));
    }
}
