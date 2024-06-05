using AspBoot.Data.Implementation;
using AspBoot.Repository;
using Microsoft.EntityFrameworkCore;
using OshService.Data;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestion;

[Repository]
public class TrainingQuestionRepository(DatabaseContext context) : Repository<TrainingQuestionModel>(context)
{
    public override IQueryable<TrainingQuestionModel> Projection(IQueryable<TrainingQuestionModel> queryable)
    {
        // queryable = queryable.Include(entity => entity.OshProgram).ThenInclude(program => program.Organization);
        queryable = queryable.Include(nameof(TrainingQuestionModel.Answers));
        return queryable;
    }

    public TrainingQuestionModel? GetById(long programId, long id, long organizationId)
    {
        return Get().FirstOrDefault(entity =>
            entity.OshProgramId == programId
            && entity.Id == id
            && entity.OshProgram.OrganizationId == organizationId);
    }

    public IQueryable<TrainingQuestionModel> OrganizationScope(long programId, long organizationId,
        IQueryable<TrainingQuestionModel> query)
    {
        return query.Where(entity =>
            entity.OshProgramId == programId
            && entity.OshProgram.OrganizationId == organizationId);
    }

    public int GetLastIndex(long programId, long organizationId)
    {
        return Get().Count(entity =>
            entity.OshProgramId == programId && entity.OshProgram.OrganizationId == organizationId) + 1;
    }
}
