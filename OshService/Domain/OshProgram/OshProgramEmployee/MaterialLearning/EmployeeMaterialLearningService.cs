using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramEmployee.MaterialLearning;

[Service]
public class EmployeeMaterialLearningService(
    LearningSectionRepository repository,
    OshProgramAssignmentRepository assignmentRepository,
    IMapper mapper,
    SecurityService service
)
{
    public object? GetAssigned(long assigmentId, RequestPage request)
    {
        var assigment = assignmentRepository.GetByEmployeeId(assigmentId, service.GetCurrentEmployeeId());
        var oshProgramId = assigment?.StartLearning != null ? assigment.OshProgram.Id : 0;
        var learnings = repository.GetPaginated(request,
            query => query.Where(e => e.OshProgramId == oshProgramId));
        return learnings.MapPage(mapper.Map<IEnumerable<LearningSectionViewRead>>);
    }

    public Result<OshProgramResultStatusEnum> GetById(long assigmentId, long sectionId)
    {
        var assigment = assignmentRepository.GetByEmployeeId(assigmentId, service.GetCurrentEmployeeId());
        if (assigment?.StartLearning == null)
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.NoPrivilegesAvailable);
        }
        var learning = repository.Get().FirstOrDefault(e => e.Id == sectionId);
        return new Result<OshProgramResultStatusEnum>(mapper.Map<LearningSectionViewRead>(learning));
    }
}
