using AspBoot.Handler;
using AspBoot.Service;
using AspBoot.Utils;
using Microsoft.EntityFrameworkCore;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.OshProgram.OshProgramEmployee.ResultLearning;
using OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Security;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ProgramResult;

[Service]
public class EmployeeProgramResultService(
    OshProgramResultRepository repository,
    OshProgramAssignmentRepository assignmentRepository,
    EmployeeResultLearningRepository learningActualRepository,
    EmployeeResultTrainingRepository trainingActualRepository,
    LearningSectionRepository learningExpectedRepository,
    TrainingQuestionRepository trainingExpectedRepository,
    SecurityService privilege)
{
    public Result<OshProgramResultStatusEnum> Result(long assigmentId)
    {
        var assigment = assignmentRepository.GetByEmployeeId(assigmentId, privilege.GetCurrentEmployeeId());
        if (assigment == null)
        {
            return new Result<OshProgramResultStatusEnum>(OshProgramResultStatusEnum.OshProgramNotFound);
        }

        var expectedLearning = learningExpectedRepository.Get()
            .Where(entity => entity.OshProgramId == assigment.OshProgramId).ToList();
        var expectedTraining = trainingExpectedRepository.Get()
            .Include(nameof(TrainingQuestionModel.Answers))
            .Where(entity => entity.OshProgramId == assigment.OshProgramId).ToList();

        var actualLearning = learningActualRepository.Get()
            .Where(entity => entity.OshProgramAssignment.Id == assigment.Id).ToList();
        var actualTraining = trainingActualRepository.Get()
            .Include(entity => entity.Answers)
            .ThenInclude(entity => entity.ActualAnswer)
            .Where(entity => entity.OshProgramAssignment.Id == assigment.Id).ToList();

        var numberExpectedLearnings = expectedLearning.Count;
        var numberActualLearnings = actualLearning
            .Count(ac => expectedLearning.Select(e => e.Id).Contains(ac.LearningSectionId));
        var learningsResult = (decimal) numberExpectedLearnings / numberActualLearnings;

        var numberExpectedTrainings = expectedTraining.Count;
        var numberActualTrainings = 0;
        foreach (var ac in actualTraining)
        {
            var question = expectedTraining.FirstOrDefault(e => e.Id == ac.TrainingQuestionId);
            var expectedAnswers = question?.Answers.Where(e => e.IsRight).Select(e => e.Id);
            var actualAnswers = ac.Answers.Select(e => e.ActualAnswer.Id);
            if (expectedAnswers != null)
            {
                if (expectedAnswers.SequenceEqual(actualAnswers))
                {
                    numberActualLearnings += 1;
                }
            }
        }
        var trainingResult = (decimal) numberExpectedTrainings / numberActualTrainings;

        var entity = new OshProgramResultModel
        {
            OshProgramAssignmentId = assigment.Id,
            LearningResult = learningsResult,
            TrainingResult = trainingResult,
            Timestamp = DateTime.Now.ToUniversalTime(),
            Id = 0,
            OshProgramAssignment = null!,
        };
        repository.Create(entity);
        return new Result<OshProgramResultStatusEnum>(entity);
    }
}
