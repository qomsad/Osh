using FluentValidation;

namespace OshService.Setup.SuperUser;

public class SetupUserValidator : AbstractValidator<SetupUserRequest>
{
    public SetupUserValidator()
    {
        RuleFor(request => request.Login).NotEmpty().MaximumLength(255).Matches(@"^[a-z0-9_]{3,}$");
        RuleFor(request => request.Password).NotEmpty().MaximumLength(255).Matches(@"^[!-~]{6,}$");
        RuleFor(request => request.Email).NotEmpty().MaximumLength(255).EmailAddress();
    }
}
