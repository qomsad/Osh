using FluentValidation;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestion;

public class TrainingQuestionValidator : AbstractValidator<TrainingQuestionViewCreate>
{
    public TrainingQuestionValidator()
    {
        RuleFor(e => e.Question).NotEmpty();
        RuleForEach(e => e.Answers).ChildRules(c =>
        {
            c.RuleFor(e => e.Index).GreaterThan(0);
            c.RuleFor(e => e.Value).NotEmpty();
        });
    }
}
