using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using OshService.Domain.OshProgram.OshProgram;
using OshService.Security;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestion;

[Service]
public class TrainingQuestionService(
    TrainingQuestionRepository repository,
    OshProgramRepository programRepository,
    IMapper mapper,
    SecurityService privilege
)
{
    public Result<TrainingQuestionStatusEnum> Create(long programId, TrainingQuestionViewCreate view)
    {
        var organizationId = privilege.GetCurrentAdministratorOrganization();
        if (organizationId == 0)
        {
            return new Result<TrainingQuestionStatusEnum>(TrainingQuestionStatusEnum.NoPrivilegesAvailable);
        }

        var program = programRepository.GetById(programId, organizationId);
        if (program == null)
        {
            return new Result<TrainingQuestionStatusEnum>(TrainingQuestionStatusEnum.OshProgramNotFound);
        }

        var entity = mapper.Map<TrainingQuestionModel>(view);
        entity.OshProgramId = program.Id;
        entity.Index = repository.GetLastIndex(programId, organizationId);
        repository.Create(entity);

        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<TrainingQuestionStatusEnum>(mapper.Map<TrainingQuestionViewRead>(result));
    }

    public object? GetPage(long programId, RequestPage request)
    {
        var page = repository.GetPaginated(request,
            query =>
            {
                query = query.OrderBy(e => e.Index);
                return repository.OrganizationScope(programId, privilege.GetCurrentAdministratorOrganization(), query);
            });

        return page.MapPage(mapper.Map<IEnumerable<TrainingQuestionViewRead>>);
    }

    public Result<TrainingQuestionStatusEnum> GetById(long programId, long sectionId)
    {
        var entity = repository.GetById(programId, sectionId, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<TrainingQuestionStatusEnum>(TrainingQuestionStatusEnum.NoPrivilegesAvailable);
        }
        return new Result<TrainingQuestionStatusEnum>(mapper.Map<TrainingQuestionViewRead>(entity));
    }

    public Result<TrainingQuestionStatusEnum> Update(long programId, long sectionId,
        TrainingQuestionViewCreate view)
    {
        var entity = repository.GetById(programId, sectionId, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<TrainingQuestionStatusEnum>(TrainingQuestionStatusEnum.NoPrivilegesAvailable);
        }
        mapper.Map(view, entity);
        repository.Update(entity);
        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<TrainingQuestionStatusEnum>(mapper.Map<TrainingQuestionViewRead>(result));
    }

    public Result<TrainingQuestionStatusEnum> Delete(long programId, long sectionId)
    {
        var entity = repository.GetById(programId, sectionId, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<TrainingQuestionStatusEnum>(TrainingQuestionStatusEnum.NoPrivilegesAvailable);
        }
        repository.Delete(entity);
        return new Result<TrainingQuestionStatusEnum>(new { });
    }
}
