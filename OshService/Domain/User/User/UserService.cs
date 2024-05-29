using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using AutoMapper;

namespace OshService.Domain.User.User;

[Service]
public class UserService(UserRepository repository, IMapper mapper)
{
    public Result<UserViewRead, UserStatusEnum> Register(UserViewCreate view)
    {
        if (repository.GetByLogin(view.Login) != null)
        {
            return new Result<UserViewRead, UserStatusEnum>(UserStatusEnum.UserLoginExists);
        }

        var (passwordHash, passwordSalt) = HashUtils.GeneratePasswordHash(view.Password);

        var user = mapper.Map<UserModel>(view);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        repository.Create(user);

        return new Result<UserViewRead, UserStatusEnum>(mapper.Map<UserViewRead>(user));
    }
}
