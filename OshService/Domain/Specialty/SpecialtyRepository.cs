using AspBoot.Data.Implementation;
using AspBoot.Repository;
using OshService.Data;

namespace OshService.Domain.Specialty;

[Repository]
public class SpecialtyRepository(DatabaseContext context) : Repository<SpecialtyModel>(context)
{
    public SpecialtyModel? GetByName(string name, long organizationId)
    {
        return Get().FirstOrDefault(entity => entity.Name == name && entity.OrganizationId == organizationId);
    }

    protected override IQueryable<SpecialtyModel> ApplySearch(string searchString, IQueryable<SpecialtyModel> query)
    {
        return query.Where(entity => entity.Name.ToLower().Contains(searchString.ToLower()));
    }

    public SpecialtyModel? GetById(long id, long organizationId)
    {
        return Get().FirstOrDefault(entity => entity.Id == id && entity.OrganizationId == organizationId);
    }

    public IQueryable<SpecialtyModel> OrganizationScope(long organizationId, IQueryable<SpecialtyModel> query)
    {
        return query.Where(entity => entity.OrganizationId == organizationId);
    }
}
