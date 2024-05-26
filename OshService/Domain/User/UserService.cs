using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using AutoMapper;
using OshService.Data;

namespace OshService.Domain.User;

[Service]
public class UserService(DatabaseContext context, IMapper mapper)
{
    public Result<UserViewRead, UserStatusEnum> Register(UserViewCreate view)
    {
        if (context.User.FirstOrDefault(e => e.Login == view.Login) != null)
        {
            return new Result<UserViewRead, UserStatusEnum>(UserStatusEnum.UserLoginExists);
        }

        var (passwordHash, passwordSalt) = HashUtils.GeneratePasswordHash(view.Password);

        var user = mapper.Map<UserModel>(view);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        context.User.Add(user);
        context.SaveChanges();

        return new Result<UserViewRead, UserStatusEnum>(mapper.Map<UserViewRead>(user));
    }
}
