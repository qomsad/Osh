using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgram;

[Service]
public class OshProgramService(OshProgramRepository repository, IMapper mapper, SecurityService privilege)
{
    public Result<OshProgramStatusEnum> Create(OshProgramViewCreate view)
    {
        var organizationId = privilege.GetCurrentAdministratorOrganization();
        if (organizationId == 0)
        {
            return new Result<OshProgramStatusEnum>(OshProgramStatusEnum.NoPrivilegesAvailable);
        }

        var entity = mapper.Map<OshProgramModel>(view);
        entity.OrganizationId = organizationId;
        repository.Create(entity);

        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<OshProgramStatusEnum>(mapper.Map<OshProgramViewRead>(result));
    }

    public PageSearched<OshProgramViewRead> Search(RequestPageSearch request)
    {
        var page = repository.Search(request,
            query => repository.OrganizationScope(privilege.GetCurrentAdministratorOrganization(), query));

        return page.MapPageSearched(mapper.Map<IEnumerable<OshProgramViewRead>>);
    }

    public Result<OshProgramStatusEnum> GetById(long id)
    {
        var entity = repository.GetById(id, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<OshProgramStatusEnum>(OshProgramStatusEnum.NoPrivilegesAvailable);
        }
        return new Result<OshProgramStatusEnum>(mapper.Map<OshProgramViewRead>(entity));
    }

    public Result<OshProgramStatusEnum> Update(long id, OshProgramViewCreate view)
    {
        var entity = repository.GetById(id, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<OshProgramStatusEnum>(OshProgramStatusEnum.NoPrivilegesAvailable);
        }
        mapper.Map(view, entity);
        repository.Update(entity);
        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<OshProgramStatusEnum>(mapper.Map<OshProgramViewRead>(result));
    }

    public Result<OshProgramStatusEnum> Delete(long id)
    {
        var entity = repository.GetById(id, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<OshProgramStatusEnum>(OshProgramStatusEnum.NoPrivilegesAvailable);
        }
        repository.Delete(entity);
        return new Result<OshProgramStatusEnum>(new { });
    }
}
