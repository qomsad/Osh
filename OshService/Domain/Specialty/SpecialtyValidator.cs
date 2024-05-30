using FluentValidation;

namespace OshService.Domain.Specialty;

public class SpecialtyValidator : AbstractValidator<SpecialtyViewCreate>
{
    public SpecialtyValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(255);
    }
}
