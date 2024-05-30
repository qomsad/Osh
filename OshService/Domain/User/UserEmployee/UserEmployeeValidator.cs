using FluentValidation;

namespace OshService.Domain.User.UserEmployee;

public class UserEmployeeValidator : AbstractValidator<UserEmployeeViewCreate>
{
    public UserEmployeeValidator()
    {
        RuleFor(request => request.Login).NotEmpty().MaximumLength(255).Matches(@"^[a-z0-9_]{3,}$");
        RuleFor(request => request.Password).NotEmpty().MaximumLength(255).Matches(@"^[!-~]{6,}$");
    }
}
