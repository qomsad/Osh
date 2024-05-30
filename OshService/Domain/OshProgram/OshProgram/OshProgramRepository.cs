using AspBoot.Data.Implementation;
using AspBoot.Repository;
using Microsoft.EntityFrameworkCore;
using OshService.Data;

namespace OshService.Domain.OshProgram.OshProgram;

[Repository]
public class OshProgramRepository(DatabaseContext context) : Repository<OshProgramModel>(context)
{
    public override IQueryable<OshProgramModel> Projection(IQueryable<OshProgramModel> queryable)
    {
        queryable = queryable.Include(nameof(OshProgramModel.Specialty));
        queryable = queryable.Include(entity => entity.LearningSections)
            .ThenInclude(sections => sections.LearningSectionFile);
        queryable = queryable.Include(entity => entity.TrainingQuestions)
            .ThenInclude(sections => sections.Answers);
        return queryable;
    }

    protected override IQueryable<OshProgramModel> ApplySearch(string searchString, IQueryable<OshProgramModel> query)
    {
        return query.Where(entity => entity.Name.ToLower().Contains(searchString.ToLower()));
    }

    public OshProgramModel? GetById(long id, long organizationId)
    {
        return Get().FirstOrDefault(entity => entity.Id == id && entity.OrganizationId == organizationId);
    }

    public IQueryable<OshProgramModel> OrganizationScope(long organizationId, IQueryable<OshProgramModel> query)
    {
        return query.Where(entity => entity.OrganizationId == organizationId);
    }
}
