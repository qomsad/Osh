using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramEmployee;

[Service]
public class EmployeeProgramService(
    OshProgramAssignmentRepository repository,
    IMapper mapper,
    SecurityService service
)
{
    public Page<EmployeeProgramViewRead> GetAssigned(RequestPage request)
    {
        var assigment = repository.GetPaginated(request,
            query => query.Where(e => e.UserEmployeeId == service.GetCurrentEmployeeId()
                                      && e.OshProgramResultId == null)
        );
        return assigment.MapPage(mapper.Map<IEnumerable<EmployeeProgramViewRead>>);
    }

    public Page<EmployeeProgramViewRead> GetResulted(RequestPage request)
    {
        var assigment = repository.GetPaginated(request,
            query => query.Where(e => e.UserEmployeeId == service.GetCurrentEmployeeId()
                                      && e.OshProgramResultId != null)
        );
        return assigment.MapPage(mapper.Map<IEnumerable<EmployeeProgramViewRead>>);
    }

    public Result<OshProgramAssignmentStatusEnum> GetById(long id)
    {
        var assigment = repository.GetByEmployeeId(id, service.GetCurrentEmployeeId());
        if (assigment == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.ProgramNotFound);
        }
        return new Result<OshProgramAssignmentStatusEnum>(mapper.Map<EmployeeProgramViewRead>(assigment));
    }

    public Result<OshProgramAssignmentStatusEnum> StartLearning(long id)
    {
        var assigment = repository.GetByEmployeeId(id, service.GetCurrentEmployeeId());
        if (assigment == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.ProgramNotFound);
        }
        if (assigment.StartLearning != null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable);
        }
        assigment.StartLearning = DateTime.Now.ToUniversalTime();
        repository.Update(assigment);
        return new Result<OshProgramAssignmentStatusEnum>(mapper.Map<EmployeeProgramViewRead>(assigment));
    }

    public Result<OshProgramAssignmentStatusEnum> StartTraining(long id)
    {
        var assigment = repository.GetByEmployeeId(id, service.GetCurrentEmployeeId());
        if (assigment == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.ProgramNotFound);
        }
        if (assigment.StartLearning == null || assigment.StartTraining != null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable);
        }
        assigment.StartTraining = DateTime.Now.ToUniversalTime();
        repository.Update(assigment);
        return new Result<OshProgramAssignmentStatusEnum>(mapper.Map<EmployeeProgramViewRead>(assigment));
    }
}
