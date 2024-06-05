using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Service;
using AutoMapper;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramResult;

[Service]
public class OshProgramResultService(
    OshProgramAssignmentRepository repository,
    IMapper mapper,
    SecurityService privilege)
{
    public object? GetResults(RequestPage request)
    {
        var page = repository.GetPaginated(request,
            query =>
            {
                query = query.Where(e => e.Result != null);
                query = query.OrderByDescending(e => e.AssignmentDate);
                return repository.OrganizationScope(privilege.GetCurrentAdministratorOrganization(), query);
            });
        return page.MapPage(mapper.Map<IEnumerable<OshProgramAssignmentViewRead>>);
    }
}
