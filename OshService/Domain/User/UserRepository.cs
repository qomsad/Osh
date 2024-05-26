using AspBoot.Data.Implementation;
using AspBoot.Repository;
using OshService.Data;

namespace OshService.Domain.User;

[Repository]
public class UserRepository(DatabaseContext context) : QueryableRepository<UserModel, long, long>(context)
{
    public UserModel? GetByLogin(string login)
    {
        return Get(entity => entity.Login == login);
    }
}
