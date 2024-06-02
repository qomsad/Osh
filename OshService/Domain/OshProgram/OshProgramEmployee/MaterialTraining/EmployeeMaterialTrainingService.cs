using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramEmployee.MaterialTraining;

[Service]
public class EmployeeMaterialTrainingService(
    TrainingQuestionRepository repository,
    OshProgramAssignmentRepository assignmentRepository,
    IMapper mapper,
    SecurityService service
)
{
    public Page<EmployeeMaterialTrainingViewRead> GetAssigned(long assigmentId, RequestPage request)
    {
        var assigment = assignmentRepository.GetByEmployeeId(assigmentId, service.GetCurrentEmployeeId());
        long oshProgramId = 0;

        if (assigment != null && assigment.StartTraining.IsLast(assigment.OshProgram.TrainingMinutesDuration))
        {
            oshProgramId = assigment.OshProgramId;
        }
        var trainings = repository.GetPaginated(request,
            query => query.Where(e => e.OshProgramId == oshProgramId));
        return trainings.MapPage(mapper.Map<IEnumerable<EmployeeMaterialTrainingViewRead>>);
    }

    public Result<OshProgramResultStatusEnum> GetById(long assigmentId, long sectionId)
    {
        var assigment = assignmentRepository.GetByEmployeeId(assigmentId, service.GetCurrentEmployeeId());
        if (assigment == null || !assigment.StartTraining.IsLast(assigment.OshProgram.TrainingMinutesDuration))
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.NoPrivilegesAvailable);
        }
        var training = repository.Get().Include(nameof(TrainingQuestionModel.Answers))
            .FirstOrDefault(e => e.Id == sectionId);
        return new Result<OshProgramResultStatusEnum>(mapper.Map<EmployeeMaterialTrainingViewRead>(training));
    }
}
