using FluentValidation;

namespace OshService.Domain.Material.MaterialLearning.LearningSection;

public class LearningSectionValidator : AbstractValidator<LearningSectionViewCreate>
{
    public LearningSectionValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(255);
        RuleFor(e => e.LearningSectionFile).ChildRules(c => c.RuleFor(e => e!.FilePath).NotEmpty());
    }
}
