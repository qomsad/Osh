using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Specialty;
using OshService.Domain.User.User;

namespace OshService.Domain.User.UserEmployee;

[Table("user_employee")]
public class UserEmployeeModel() : UserModel(UserType.Employee)
{
    [Column("service_number"), MaxLength(255)]
    public string? ServiceNumber { get; set; }

    [Column("speciality_id")]
    public required long SpecialityId { get; set; }

    [ForeignKey(nameof(SpecialityId))]
    public required SpecialtyModel Specialty { get; set; }
}
