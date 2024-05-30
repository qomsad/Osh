using AspBoot.Data.Implementation;
using AspBoot.Repository;
using OshService.Data;

namespace OshService.Domain.User.UserAdministrator;

[Repository]
public class UserAdministratorRepository(DatabaseContext context) : Repository<UserAdministratorModel>(context)
{
    public UserAdministratorModel? GetByLogin(string login)
    {
        return Get().FirstOrDefault(entity => entity.Login == login);
    }
}
