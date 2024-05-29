namespace OshService.Domain.User.UserAdministrator;

public class UserAdministratorViewRead
{
    public required string Login { get; set; }

    public required string Email { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }
}
