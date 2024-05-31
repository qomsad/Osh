using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public Page<OshProgramAssignmentViewRead>? GetAssigned(RequestPage request)
    {
        var employee = service.GetCurrentEmployee();
        if (employee != null)
        {
            var assigment = repository.GetPaginated(request, query => query
                .Where(e => e.UserEmployeeId == employee.Id && e.OshProgramResultId == null)
            );

            return assigment.MapPage(mapper.Map<IEnumerable<OshProgramAssignmentViewRead>>);
        }
        return null;
    }

    public Page<OshProgramAssignmentViewRead>? GetResult(RequestPage request)
    {
        var employee = service.GetCurrentEmployee();
        if (employee != null)
        {
            var assigment = repository.GetPaginated(request, query => query
                .Where(e => e.UserEmployeeId == employee.Id && e.OshProgramResultId != null));

            return assigment.MapPage(mapper.Map<IEnumerable<OshProgramAssignmentViewRead>>);
        }
        return null;
    }

    public Result<OshProgramAssignmentStatusEnum> GetById(long id)
    {
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable);
        }
        var assigment = repository.Get().Include(nameof(OshProgramAssignmentModel.OshProgram))
            .FirstOrDefault(e => e.Employee.Id == employee.Id && e.Id == id);
        if (assigment == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.ProgramNotFound);
        }
        return new Result<OshProgramAssignmentStatusEnum>(mapper.Map<OshProgramAssignmentViewRead>(assigment));
    }

    public Result<OshProgramAssignmentStatusEnum> StartLearning(long id)
    {
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable);
        }
        var assigment = repository.Get().Include(nameof(OshProgramAssignmentModel.OshProgram))
            .FirstOrDefault(e => e.Employee.Id == employee.Id && e.Id == id);
        if (assigment == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.ProgramNotFound);
        }
        assigment.StartLearning = DateTime.Now.ToUniversalTime();
        return new Result<OshProgramAssignmentStatusEnum>(mapper.Map<OshProgramAssignmentViewRead>(assigment));
    }

    public Result<OshProgramAssignmentStatusEnum> StartTraining(long id)
    {
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable);
        }
        var assigment = repository.Get().Include(nameof(OshProgramAssignmentModel.OshProgram))
            .FirstOrDefault(e => e.Employee.Id == employee.Id && e.Id == id);
        if (assigment == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.ProgramNotFound);
        }
        assigment.StartTraining = DateTime.Now.ToUniversalTime();
        return new Result<OshProgramAssignmentStatusEnum>(mapper.Map<OshProgramAssignmentViewRead>(assigment));
    }
}
