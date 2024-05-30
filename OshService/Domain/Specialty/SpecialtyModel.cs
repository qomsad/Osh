using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Organization;
using OshService.Domain.User.UserEmployee;

namespace OshService.Domain.Specialty;

[Table("specialties")]
public class SpecialtyModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("organization_id")]
    public required long OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public OrganizationModel Organization { get; set; } = null!;

    [Column("name"), MaxLength(255)]
    public required string Name { get; set; }

    [Column("created")]
    public required DateTime Created { get; set; }

    [Column("updated")]
    public required DateTime Updated { get; set; }

    public required IEnumerable<UserEmployeeModel> Employees { get; set; }
}
