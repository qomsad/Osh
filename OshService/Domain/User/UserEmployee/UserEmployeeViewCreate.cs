namespace OshService.Domain.User.UserEmployee;

public class UserEmployeeViewCreate
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? ServiceNumber { get; set; }

    public required long SpecialtyId { get; set; }
}
