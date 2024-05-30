using AspBoot.Data.Implementation;
using AspBoot.Data.Model;
using AspBoot.Repository;
using Microsoft.EntityFrameworkCore;
using OshService.Data;

namespace OshService.Domain.User.UserEmployee;

[Repository]
public class UserEmployeeRepository(DatabaseContext context) : Repository<UserEmployeeModel>(context)
{
    public override IQueryable<UserEmployeeModel> Projection(IQueryable<UserEmployeeModel> query)
    {
        query = query.Include(nameof(UserEmployeeModel.Organization));
        query = query.Include(nameof(UserEmployeeModel.Specialty));
        return query;
    }

    public UserEmployeeModel? GetByLogin(string login)
    {
        return Get().FirstOrDefault(entity => entity.Login == login);
    }

    protected override IQueryable<UserEmployeeModel> ApplySearch(string searchString,
        IQueryable<UserEmployeeModel> query)
    {
        return query.Where(entity =>
            entity.FirstName != null && entity.FirstName.ToLower().Contains(searchString.ToLower())
            || entity.MiddleName != null && entity.MiddleName.ToLower().Contains(searchString.ToLower())
            || entity.LastName != null && entity.LastName.ToLower().Contains(searchString.ToLower())
            || entity.ServiceNumber != null && entity.ServiceNumber.ToLower().Contains(searchString.ToLower())
        );
    }

    protected override IQueryable<UserEmployeeModel> ApplyFiltering(IQueryable<UserEmployeeModel> query,
        Filter.Predicate? filter)
    {
        if (filter != null
            && filter.Selector.ToLower().Equals("Speciality".ToLower())
            && filter.Operator == Filter.Operator.In)
        {
            var values = filter.Values.Split(";");
            var ids = new long[values.Length];
            if (values.Any())
            {
                for (var i = 0; i < values.Length; i++)
                {
                    Int64.TryParse(values[i], out var val);
                    ids[i] = val;
                }
            }
            query = query.Where(e => ids.Contains(e.SpecialityId));
        }
        return query;
    }

    public UserEmployeeModel? GetById(long id, long organizationId)
    {
        return Get().FirstOrDefault(entity => entity.Id == id && entity.OrganizationId == organizationId);
    }

    public IQueryable<UserEmployeeModel> OrganizationScope(long organizationId, IQueryable<UserEmployeeModel> query)
    {
        return query.Where(entity => entity.OrganizationId == organizationId);
    }
}
