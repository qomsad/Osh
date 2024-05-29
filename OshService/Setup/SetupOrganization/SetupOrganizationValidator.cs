using FluentValidation;

namespace OshService.Setup.SetupOrganization;

public class SetupOrganizationValidator : AbstractValidator<SetupOrganizationRequest>
{
    public SetupOrganizationValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(255);
        RuleFor(e => e.Url).NotEmpty().MaximumLength(255)
            .Matches(@"^[a-z_][a-z0-9_]*(\.[a-z_][a-z0-9_]*)*$");
    }
}
