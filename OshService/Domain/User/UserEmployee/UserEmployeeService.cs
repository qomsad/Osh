using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using AutoMapper;
using OshService.Domain.Specialty;
using OshService.Domain.User.User;
using OshService.Security;

namespace OshService.Domain.User.UserEmployee;

[Service]
public class UserEmployeeService(
    UserEmployeeRepository repository,
    SpecialtyRepository specialtyRepository,
    UserRepository userRepository,
    IMapper mapper,
    SecurityService privilege
)
{
    public Result<UserEmployeeStatusEnum> Create(UserEmployeeViewCreate view)
    {
        var organizationId = privilege.GetCurrentAdministratorOrganization();
        if (organizationId == 0)
        {
            return new Result<UserEmployeeStatusEnum>(UserEmployeeStatusEnum.NoPrivilegesAvailable);
        }

        var specialty = specialtyRepository.GetById(view.SpecialtyId, organizationId);
        if (specialty == null)
        {
            return new Result<UserEmployeeStatusEnum>(UserEmployeeStatusEnum.SpecialtyNotFound);
        }

        if (userRepository.GetByLogin(view.Login) != null)
        {
            return new Result<UserEmployeeStatusEnum>(UserEmployeeStatusEnum.UserLoginExists);
        }

        var entity = mapper.Map<UserEmployeeModel>(view);
        var (passwordHash, passwordSalt) = HashUtils.GeneratePasswordHash(view.Password);
        entity.PasswordHash = passwordHash;
        entity.PasswordSalt = passwordSalt;
        entity.OrganizationId = organizationId;
        entity.SpecialityId = specialty.Id;
        repository.Create(entity);

        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<UserEmployeeStatusEnum>(mapper.Map<UserEmployeeViewRead>(result));
    }

    public PageSearched<UserEmployeeViewRead> Search(RequestPageSearch request)
    {
        var page = repository.Search(request,
            query =>
            {
                query = query.OrderBy(e => e.Id);
                return repository.OrganizationScope(privilege.GetCurrentAdministratorOrganization(), query);
            });

        return page.MapPageSearched(mapper.Map<IEnumerable<UserEmployeeViewRead>>);
    }

    public object? Filter(RequestPageSortedFiltered request)
    {
        var page = repository.GetFiltered(request,
            query => repository.OrganizationScope(privilege.GetCurrentAdministratorOrganization(), query));

        return page.MapPageSortedFiltered(mapper.Map<IEnumerable<UserEmployeeViewRead>>);
    }

    public Result<UserEmployeeStatusEnum> GetById(long id)
    {
        var entity = repository.GetById(id, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<UserEmployeeStatusEnum>(UserEmployeeStatusEnum.NoPrivilegesAvailable);
        }
        return new Result<UserEmployeeStatusEnum>(mapper.Map<UserEmployeeViewRead>(entity));
    }

    public Result<UserEmployeeStatusEnum> Update(long id, UserEmployeeViewUpdate view)
    {
        var organizationId = privilege.GetCurrentAdministratorOrganization();
        var entity = repository.GetById(id, organizationId);
        if (entity == null)
        {
            return new Result<UserEmployeeStatusEnum>(UserEmployeeStatusEnum.NoPrivilegesAvailable);
        }

        var specialty = specialtyRepository.GetById(view.SpecialtyId, organizationId);
        if (specialty == null)
        {
            return new Result<UserEmployeeStatusEnum>(UserEmployeeStatusEnum.SpecialtyNotFound);
        }
        mapper.Map(view, entity);
        if (view.Password != null)
        {
            var (passwordHash, passwordSalt) = HashUtils.GeneratePasswordHash(view.Password);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
        }
        entity.SpecialityId = specialty.Id;
        repository.Update(entity);
        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<UserEmployeeStatusEnum>(mapper.Map<UserEmployeeViewRead>(result));
    }

    public Result<UserEmployeeStatusEnum> Delete(long id)
    {
        var entity = repository.GetById(id, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<UserEmployeeStatusEnum>(UserEmployeeStatusEnum.NoPrivilegesAvailable);
        }
        repository.Delete(entity);
        return new Result<UserEmployeeStatusEnum>(new { });
    }
}
