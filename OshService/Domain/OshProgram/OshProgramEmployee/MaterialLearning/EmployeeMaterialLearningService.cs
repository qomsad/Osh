using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramEmployee.MaterialLearning;

[Service]
public class EmployeeMaterialLearningService(
    LearningSectionRepository repository,
    OshProgramAssignmentRepository assignmentRepository,
    IMapper mapper,
    SecurityService service)
{
    public object? GetAssigned(long assigmentId, RequestPage request)
    {
        var employee = service.GetCurrentEmployee();
        if (employee != null)
        {
            var assigment = assignmentRepository.Get()
                .FirstOrDefault(e => e.UserEmployeeId == employee.Id && e.Id == assigmentId);
            if (assigment != null)
            {
                var learnings =
                    repository.GetPaginated(request,
                        query => query.Where(e => e.OshProgramId == assigment.OshProgramId));
                return learnings.MapPage(mapper.Map<IEnumerable<LearningSectionViewRead>>);
            }
        }
        return null;
    }

    public Result<LearningSectionStatusEnum> GetById(long assigmentId, long sectionId)
    {
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new
                Result<LearningSectionStatusEnum>(LearningSectionStatusEnum.NoPrivilegesAvailable);
        }
        var assigment = assignmentRepository.Get()
            .FirstOrDefault(e => e.UserEmployeeId == employee.Id && e.Id == assigmentId);
        if (assigment == null)
        {
            return new Result<LearningSectionStatusEnum>
                (LearningSectionStatusEnum.OshProgramNotFound);
        }
        {
            var learning = repository.Get().FirstOrDefault(e => e.Id == sectionId);
            return new Result<LearningSectionStatusEnum>(
                mapper.Map<LearningSectionViewRead>(learning));
        }
    }
}
