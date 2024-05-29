using OshService.Domain.User.UserAdministrator;

namespace OshService.Setup.SetupOrganization;

public class SetupOrganizationResponse
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }

    public required UserAdministratorViewRead Administrator { get; set; }
}
