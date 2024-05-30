using FluentValidation;

namespace OshService.Domain.OshProgram.OshProgram;

public class OshProgramValidator : AbstractValidator<OshProgramViewCreate>
{
    public OshProgramValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(255);
        RuleFor(e => e.MaxAutoAssignments).GreaterThan(0);
        RuleFor(e => e.LearningMinutesDuration).GreaterThan(0);
        RuleFor(e => e.TrainingMinutesDuration).GreaterThan(0);
    }
}
