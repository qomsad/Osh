using AspBoot.Data.Implementation;
using AspBoot.Repository;
using Microsoft.EntityFrameworkCore;
using OshService.Data;

namespace OshService.Domain.Material.MaterialLearning.LearningSection;

[Repository]
public class LearningSectionRepository(DatabaseContext context) : Repository<LearningSectionModel>(context)
{
    public override IQueryable<LearningSectionModel> Projection(IQueryable<LearningSectionModel> queryable)
    {
        // queryable = queryable.Include(entity => entity.OshProgram).ThenInclude(program => program.Organization);
        queryable = queryable.Include(nameof(LearningSectionModel.LearningSectionFile));
        return queryable;
    }

    public LearningSectionModel? GetById(long programId, long id, long organizationId)
    {
        return Get().FirstOrDefault(entity =>
            entity.OshProgramId == programId
            && entity.Id == id
            && entity.OshProgram.OrganizationId == organizationId);
    }

    public IQueryable<LearningSectionModel> OrganizationScope(long programId, long organizationId,
        IQueryable<LearningSectionModel> query)
    {
        return query.Where(entity =>
            entity.OshProgramId == programId
            && entity.OshProgram.OrganizationId == organizationId);
    }
}
