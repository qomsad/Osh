namespace OshService.Domain.User.UserEmployee;

public class UserEmployeeViewUpdate
{
    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? ServiceNumber { get; set; }

    public required long SpecialtyId { get; set; }

    public string? Password { get; set; }
}
