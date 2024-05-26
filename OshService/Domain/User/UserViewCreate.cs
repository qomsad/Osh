namespace OshService.Domain.User;

public class UserViewCreate
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }
}
