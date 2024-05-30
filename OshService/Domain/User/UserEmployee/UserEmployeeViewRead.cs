using OshService.Domain.Specialty;
using OshService.Domain.User.User;

namespace OshService.Domain.User.UserEmployee;

public class UserEmployeeViewRead
{
    public long Id { get; set; }

    public UserType? Type { set; get; }

    public required string Login { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? ServiceNumber { get; set; }

    public SpecialtyViewRead? Specialty { get; set; }
}
