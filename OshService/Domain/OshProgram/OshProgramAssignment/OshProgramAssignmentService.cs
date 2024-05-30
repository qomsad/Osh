using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using OshService.Domain.OshProgram.OshProgram;
using OshService.Domain.User.UserEmployee;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramAssignment;

[Service]
public class OshProgramAssignmentService(
    OshProgramAssignmentRepository repository,
    UserEmployeeRepository userRepository,
    OshProgramRepository programRepository,
    IMapper mapper,
    SecurityService privilege
)
{
    public Result<OshProgramAssignmentStatusEnum> Create(OshProgramAssignmentViewCreate view)
    {
        var organizationId = privilege.GetCurrentAdministratorOrganization();
        if (organizationId == 0)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable);
        }

        var user = userRepository.GetById(view.UserEmployeeId, organizationId);
        if (user == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.EmployeeNotFound);
        }

        var program = programRepository.GetById(view.OshProgramId, organizationId);
        if (program == null)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.ProgramNotFound);
        }

        var entity = mapper.Map<OshProgramAssignmentModel>(view);
        entity.UserEmployeeId = user.Id;
        entity.OshProgramId = program.Id;
        entity.AssignmentDate = DateTime.Now.ToUniversalTime();

        repository.Create(entity);

        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<OshProgramAssignmentStatusEnum>(mapper.Map<OshProgramAssignmentViewRead>(result));
    }

    public object? GetFiltered(RequestPageSortedFiltered request)
    {
        var page = repository.GetFiltered(request,
            query => repository.OrganizationScope(privilege.GetCurrentAdministratorOrganization(), query));
        return page.MapPageSortedFiltered(mapper.Map<IEnumerable<OshProgramAssignmentViewRead>>);
    }

    public Result<OshProgramAssignmentStatusEnum> GetById(long id)
    {
        var organizationId = privilege.GetCurrentAdministratorOrganization();
        if (organizationId == 0)
        {
            return new Result<OshProgramAssignmentStatusEnum>(OshProgramAssignmentStatusEnum.NoPrivilegesAvailable);
        }

        var entity = repository.GetById(id, organizationId);
        return new Result<OshProgramAssignmentStatusEnum>(mapper.Map<OshProgramAssignmentViewRead>(entity));
    }
}
