using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using AutoMapper;
using OshService.Auth;
using OshService.Domain.User.User;
using OshService.Domain.User.UserAdministrator;
using OshService.Setup.SetupPrivilege;

namespace OshService.Setup.SuperUser;

[Service]
public class SetupUserService(
    UserAdministratorRepository repository,
    UserRepository userRepository,
    IMapper mapper,
    SetupPrivilegeRepository privilege,
    AuthService auth
)
{
    public Result<SetupUserStatusEnum> CreateSuperUser(SetupUserRequest request)
    {
        if (!privilege.IsSetupAvailable())
        {
            return new Result<SetupUserStatusEnum>(SetupUserStatusEnum.NoPrivilegesAvailable);
        }

        if (userRepository.GetByLogin(request.Login) != null)
        {
            return new Result<SetupUserStatusEnum>(SetupUserStatusEnum.UserLoginExists);
        }

        var entity = mapper.Map<UserAdministratorModel>(request);
        var (passwordHash, passwordSalt) = HashUtils.GeneratePasswordHash(request.Password);
        entity.PasswordHash = passwordHash;
        entity.PasswordSalt = passwordSalt;

        repository.Create(entity);

        var result = mapper.Map<SetupUserResponse>(entity);
        result.JwtToken = auth.GenerateToken(entity.Login, entity.Type.ToString());
        return new Result<SetupUserStatusEnum>(result);
    }
}
