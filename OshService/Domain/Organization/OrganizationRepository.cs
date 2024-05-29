using AspBoot.Data.Implementation;
using AspBoot.Repository;
using Microsoft.EntityFrameworkCore;
using OshService.Data;

namespace OshService.Domain.Organization;

[Repository]
public class OrganizationRepository(DatabaseContext context) : Repository<OrganizationModel, long>(context)
{
    public override IQueryable<OrganizationModel> Projection(IQueryable<OrganizationModel> query)
    {
        return query.Include(nameof(OrganizationModel.Administrator));
    }

    public OrganizationModel? GetByUrl(string url)
    {
        return GetOne(q => q.Where(entity => entity.Url == url));
    }
}
