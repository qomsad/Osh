using AspBoot.Data.Implementation;
using AspBoot.Repository;
using Microsoft.EntityFrameworkCore;
using OshService.Data;

namespace OshService.Domain.User.UserEmployee;

[Repository]
public class UserEmployeeRepository(DatabaseContext context) : Repository<UserEmployeeModel>(context)
{
    public override IQueryable<UserEmployeeModel> Projection(IQueryable<UserEmployeeModel> query)
    {
        return query.Include(nameof(UserEmployeeModel.Organization));
    }

    public UserEmployeeModel? GetByLogin(string login)
    {
        return Get().FirstOrDefault(entity => entity.Login == login);
    }
}
