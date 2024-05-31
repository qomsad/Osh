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
    public Page<EmployeeProgramViewRead>? GetAssigned(RequestPage request)
    {
        var employee = service.GetCurrentEmployee();
        if (employee != null)
        {
            var programs = repository.GetPaginated(request, query => query
                .Where(e => e.UserEmployeeId == employee.Id));

            return programs.MapPage(mapper.Map<IEnumerable<EmployeeProgramViewRead>>);
        }
        return null;
    }

    public Result<EmployeeProgramStatusEnum> GetById(long id)
    {
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new Result<EmployeeProgramStatusEnum>(EmployeeProgramStatusEnum.NoPrivilegesAvailable);
        }
        var program = repository.Get().Include(nameof(OshProgramAssignmentModel.OshProgram))
            .FirstOrDefault(e => e.Employee.Id == employee.Id && e.Id == id);
        if (program == null)
        {
            return new Result<EmployeeProgramStatusEnum>(EmployeeProgramStatusEnum.OshProgramNotFound);
        }
        return new Result<EmployeeProgramStatusEnum>(mapper.Map<EmployeeProgramViewRead>(program));
    }
}
