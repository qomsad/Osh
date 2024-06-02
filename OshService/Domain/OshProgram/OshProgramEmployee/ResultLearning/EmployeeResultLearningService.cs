using AspBoot.Handler;
using AspBoot.Service;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.OshProgram.OshProgramResult;
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
    public Result<OshProgramResultStatusEnum> Result(long assigmentId, long questionId)
    {
        var assigment = assignmentRepository.GetByEmployeeId(assigmentId, service.GetCurrentEmployeeId());
        if (assigment == null)
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.OshProgramNotFound);
        }
        var question = learningSectionRepository.Get().FirstOrDefault(e => e.Id == questionId);
        if (question == null)
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.OshProgramNotFound);
        }
        if (assigment.StartLearning == null)
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.NoPrivilegesAvailable);
        }
        var entity = new EmployeeResultLearningModel
        {
            LearningSectionId = question.Id,
            OshProgramAssignmentId = assigmentId,
            Timestamp = DateTime.Now.ToUniversalTime(),
            Id = 0,
            LearningSection = null!,
            OshProgramAssignment = null!
        };

        repository.Create(entity);
        return new Result<OshProgramResultStatusEnum>((new { entity.Id, entity.Timestamp }));
    }
}
