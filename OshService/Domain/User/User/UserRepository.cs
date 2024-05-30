using AspBoot.Data.Implementation;
using AspBoot.Repository;
using OshService.Data;

namespace OshService.Domain.User.User;

[Repository]
public class UserRepository(DatabaseContext context) : Repository<UserModel, long>(context)
{
    public UserModel? GetByLogin(string login)
    {
        return Get().FirstOrDefault(entity => entity.Login == login);
    }
}
