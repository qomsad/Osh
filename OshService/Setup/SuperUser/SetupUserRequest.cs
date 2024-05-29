namespace OshService.Setup.SuperUser;

public class SetupUserRequest
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public required string Email { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }
}
