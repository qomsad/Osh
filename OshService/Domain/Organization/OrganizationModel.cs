using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.User.UserAdministrator;

namespace OshService.Domain.Organization;

[Table("organization")]
public class OrganizationModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("name"), MaxLength(255)]
    public required string Name { get; set; }

    [Column("url"), MaxLength(255)]
    public required string Url { get; set; }

    [Column("user_administrator_id")]
    public required long UserAdministratorId { get; set; }

    [ForeignKey(nameof(UserAdministratorId))]
    public required UserAdministratorModel Administrator { get; set; }
}
