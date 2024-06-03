using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OshService.Security;

namespace OshService.Domain.Specialty;

[Service]
public class SpecialtyService(SpecialtyRepository repository, IMapper mapper, SecurityService privilege)
{
    public Result<SpecialtyStatusEnum> Create(SpecialtyViewCreate view)
    {
        var organizationId = privilege.GetCurrentAdministratorOrganization();
        if (organizationId == 0)
        {
            return new Result<SpecialtyStatusEnum>(SpecialtyStatusEnum.NoPrivilegesAvailable);
        }

        var name = view.Name.Replace(" ", "").Replace("\t", "").Replace("\n", "");
        if (repository.GetByName(name, organizationId) != null)
        {
            return new Result<SpecialtyStatusEnum>(SpecialtyStatusEnum.SpecialtyAlreadyExists);
        }

        var entity = mapper.Map<SpecialtyModel>(view);
        entity.OrganizationId = organizationId;
        entity.Created = DateTime.Now.ToUniversalTime();
        entity.Updated = DateTime.Now.ToUniversalTime();
        repository.Create(entity);

        return new Result<SpecialtyStatusEnum>(mapper.Map<SpecialtyViewRead>(entity));
    }

    public PageSearched<SpecialtyViewRead> Search(RequestPageSearch request)
    {
        var page = repository.Search(request,
            query =>
            {
                query = query.OrderBy(e => e.Created);
                return repository.OrganizationScope(privilege.GetCurrentAdministratorOrganization(), query);
            });

        return page.MapPageSearched(mapper.Map<IEnumerable<SpecialtyViewRead>>);
    }

    public Result<SpecialtyStatusEnum> GetById(long id)
    {
        var entity = repository.GetById(id, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<SpecialtyStatusEnum>(SpecialtyStatusEnum.NoPrivilegesAvailable);
        }
        return new Result<SpecialtyStatusEnum>(mapper.Map<SpecialtyViewRead>(entity));
    }

    public Result<SpecialtyStatusEnum> Update(long id, SpecialtyViewCreate view)
    {
        var entity = repository.GetById(id, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<SpecialtyStatusEnum>(SpecialtyStatusEnum.NoPrivilegesAvailable);
        }
        mapper.Map(view, entity);
        entity.Updated = DateTime.Now.ToUniversalTime();
        repository.Update(entity);
        return new Result<SpecialtyStatusEnum>(mapper.Map<SpecialtyViewRead>(entity));
    }

    public Result<SpecialtyStatusEnum> Delete(long id)
    {
        var entity = repository.GetById(id, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<SpecialtyStatusEnum>(SpecialtyStatusEnum.NoPrivilegesAvailable);
        }
        try
        {
            repository.Delete(entity);
        }
        catch (DbUpdateException)
        {
            return new Result<SpecialtyStatusEnum>(SpecialtyStatusEnum.DeleteLock);
        }
        return new Result<SpecialtyStatusEnum>(new { });
    }
}
