using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using OshService.Domain.Organization;
using OshService.Domain.User.User;
using OshService.Domain.User.UserAdministrator;
using OshService.Setup.SetupPrivilege;

namespace OshService.Setup.SetupOrganization;

[Service]
public class SetupOrganizationService(
    OrganizationRepository repository,
    IMapper mapper,
    SetupPrivilegeRepository privilege,
    UserAdministratorRepository userRepository
)
{
    public Result<SetupOrganizationStatusEnum> CreateOrganization(string login, SetupOrganizationRequest request)
    {
        var admin = userRepository.GetByLogin(login);
        if (admin is not { Type: UserType.Admin } || !privilege.IsSetupAvailable())
        {
            return new Result<SetupOrganizationStatusEnum>(SetupOrganizationStatusEnum.NoPrivilegesAvailable);
        }
        if (repository.GetByUrl(request.Url) != null)
        {
            return new Result<SetupOrganizationStatusEnum>(SetupOrganizationStatusEnum.OrganizationUrlExists);
        }

        var entity = new OrganizationModel
        {
            Name = request.Name,
            Url = request.Url,
            UserAdministratorId = admin.Id
        };
        repository.Create(entity);
        admin.ManageOrganizationId = entity.Id;
        userRepository.Update(admin);

        privilege.SetupLock();

        var result = repository.Get(entity);
        return new Result<SetupOrganizationStatusEnum>(mapper.Map<SetupOrganizationResponse>(result));
    }
}
