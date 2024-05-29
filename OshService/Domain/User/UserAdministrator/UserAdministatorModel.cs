using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Organization;
using OshService.Domain.User.User;

namespace OshService.Domain.User.UserAdministrator;

[Table("user_administrator")]
public class UserAdministratorModel() : UserModel(UserType.Admin)
{
    [Column("position"), MaxLength(255)]
    public required string Position { get; set; }

    [Column("department"), MaxLength(255)]
    public string? Department { get; set; }

    [Column("manage_organization_id")]
    public long? ManageOrganizationId { get; set; }

    [ForeignKey(nameof(ManageOrganizationId))]
    public OrganizationModel? ManageOrganization { get; set; }
}
