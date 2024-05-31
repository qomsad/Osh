using AspBoot.Data.Model;
using AspBoot.Data.Request;
using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.OshProgram.OshProgramAssignment;
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
    public object? GetAssigned(long assigmentId, RequestPage request)
    {
        var employee = service.GetCurrentEmployee();
        if (employee != null)
        {
            var assigment = assignmentRepository.Get()
                .Include(nameof(OshProgramAssignmentModel.OshProgram))
                .FirstOrDefault(
                    e => e.UserEmployeeId == employee.Id
                         && e.Id == assigmentId
                         && e.Result == null);
            if (assigment != null &&
                assigment.StartTraining.IsLast(assigment.OshProgram.TrainingMinutesDuration))
            {
                var trainings =
                    repository.GetPaginated(request,
                        query => query.Where(e => e.OshProgramId == assigment.OshProgramId));
                return trainings.MapPage(mapper.Map<IEnumerable<EmployeeMaterialTrainingViewRead>>);
            }
        }
        return null;
    }

    public Result<TrainingQuestionStatusEnum> GetById(long assigmentId, long sectionId)
    {
        var employee = service.GetCurrentEmployee();
        if (employee == null)
        {
            return new
                Result<TrainingQuestionStatusEnum>(TrainingQuestionStatusEnum.NoPrivilegesAvailable);
        }
        var assigment = assignmentRepository.Get()
            .Include(nameof(OshProgramAssignmentModel.OshProgram))
            .FirstOrDefault(
                e => e.UserEmployeeId == employee.Id
                     && e.Id == assigmentId
                     && e.Result == null
                     && e.StartTraining != null);
        if (assigment != null &&
            assigment.StartTraining.IsLast(assigment.OshProgram.TrainingMinutesDuration))
        {
            return new Result<TrainingQuestionStatusEnum>
                (TrainingQuestionStatusEnum.OshProgramNotFound);
        }
        var training = repository.Get().Include(nameof(TrainingQuestionModel.Answers))
            .FirstOrDefault(e => e.Id == sectionId);
        return new Result<TrainingQuestionStatusEnum>(
            mapper.Map<EmployeeMaterialTrainingViewRead>(training));
    }
}
