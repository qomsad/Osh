using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Organization;
using OshService.Domain.Specialty;
using OshService.Domain.User.User;

namespace OshService.Domain.User.UserEmployee;

[Table("user_employee")]
public class UserEmployeeModel() : UserModel(UserType.Employee)
{
    [Column("service_number"), MaxLength(255)]
    public string? ServiceNumber { get; set; }

    [Column("organization_id")]
    public required long OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public OrganizationModel Organization { get; set; } = null!;

    [Column("speciality_id")]
    public required long SpecialityId { get; set; }

    [ForeignKey(nameof(SpecialityId))]
    public SpecialtyModel Specialty { get; set; } = null!;
}
