using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AutoMapper;
using OshService.Domain.OshProgram.OshProgram;
using OshService.Security;

namespace OshService.Domain.Material.MaterialLearning.LearningSection;

[Service]
public class LearningSectionService(
    LearningSectionRepository repository,
    OshProgramRepository programRepository,
    IMapper mapper,
    SecurityService privilege
)
{
    public Result<LearningSectionStatusEnum> Create(long programId, LearningSectionViewCreate view)
    {
        var organizationId = privilege.GetCurrentAdministratorOrganization();
        if (organizationId == 0)
        {
            return new Result<LearningSectionStatusEnum>(LearningSectionStatusEnum.NoPrivilegesAvailable);
        }

        var program = programRepository.GetById(programId, organizationId);
        if (program == null)
        {
            return new Result<LearningSectionStatusEnum>(LearningSectionStatusEnum.OshProgramNotFound);
        }

        var entity = mapper.Map<LearningSectionModel>(view);
        entity.OshProgramId = program.Id;
        entity.Index = repository.GetLastIndex(programId, organizationId);
        repository.Create(entity);

        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<LearningSectionStatusEnum>(mapper.Map<LearningSectionViewRead>(result));
    }

    public object? GetPage(long programId, RequestPage request)
    {
        var page = repository.GetPaginated(request,
            query =>
            {
                query = query.OrderBy(e => e.Index);
                return repository.OrganizationScope(programId, privilege.GetCurrentAdministratorOrganization(), query);
            });

        return page.MapPage(mapper.Map<IEnumerable<LearningSectionViewRead>>);
    }

    public Result<LearningSectionStatusEnum> GetById(long programId, long sectionId)
    {
        var entity = repository.GetById(programId, sectionId, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<LearningSectionStatusEnum>(OshProgramStatusEnum.NoPrivilegesAvailable);
        }
        return new Result<LearningSectionStatusEnum>(mapper.Map<LearningSectionViewRead>(entity));
    }

    public Result<LearningSectionStatusEnum> Update(long programId, long sectionId,
        LearningSectionViewCreate view)
    {
        var entity = repository.GetById(programId, sectionId, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<LearningSectionStatusEnum>(OshProgramStatusEnum.NoPrivilegesAvailable);
        }
        mapper.Map(view, entity);
        repository.Update(entity);
        var result = repository.Get().FirstOrDefault(e => e.Id == entity.Id);
        return new Result<LearningSectionStatusEnum>(mapper.Map<LearningSectionViewRead>(result));
    }

    public Result<LearningSectionStatusEnum> Delete(long programId, long sectionId)
    {
        var entity = repository.GetById(programId, sectionId, privilege.GetCurrentAdministratorOrganization());
        if (entity == null)
        {
            return new Result<LearningSectionStatusEnum>(OshProgramStatusEnum.NoPrivilegesAvailable);
        }
        repository.Delete(entity);
        return new Result<LearningSectionStatusEnum>(new { });
    }
}
