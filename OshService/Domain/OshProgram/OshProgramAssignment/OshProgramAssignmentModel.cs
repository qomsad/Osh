using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.OshProgram.OshProgram;
using OshService.Domain.User.UserEmployee;

namespace OshService.Domain.OshProgram.OshProgramAssignment;

[Table("program_assigment")]
public class OshProgramAssignmentModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("user_employee_id")]
    public required long UserEmployeeId { get; set; }

    [ForeignKey(nameof(UserEmployeeId))]
    public required UserEmployeeModel Employee { get; set; }

    [Column("program_id")]
    public required long OshProgramId { get; set; }

    [ForeignKey(nameof(OshProgramId))]
    public required OshProgramModel OshProgram { get; set; }

    [Column("assignment_date")]
    public required DateTime AssignmentDate { get; set; }
}
