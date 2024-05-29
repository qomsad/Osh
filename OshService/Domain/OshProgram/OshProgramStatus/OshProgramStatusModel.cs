using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.OshProgram.OshProgramAssignment;

namespace OshService.Domain.OshProgram.OshProgramStatus;

[Table("program_status")]
public class OshProgramStatusModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("program_assignment_id")]
    public long OshProgramAssignmentId { get; set; }

    [ForeignKey(nameof(OshProgramAssignmentId))]
    public required OshProgramAssignmentModel OshProgramAssignment { get; set; }

    [Column("global_status")]
    public required OshProgramGlobalStatus GlobalStatus { get; set; }

    [Column("timestamp")]
    public required DateTime Timestamp { get; set; }
}

public enum OshProgramGlobalStatus
{
    StartLearning,
    EndLearning,
    StartTraining,
    EndTraining,
}
