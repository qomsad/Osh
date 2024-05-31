using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OshService.Domain.OshProgram.OshProgramAssignment;

namespace OshService.Domain.OshProgram.OshProgramResult;

[Table("program_result")]
public class OshProgramResultModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("program_assignment_id")]
    public long OshProgramAssignmentId { get; set; }

    [ForeignKey(nameof(OshProgramAssignmentId))]
    public required OshProgramAssignmentModel OshProgramAssignment { get; set; }

    [Column("learning_result"), Precision(4, 2)]
    public required decimal LearningResult { get; set; }

    [Column("training_result"), Precision(4, 2)]
    public required decimal TrainingResult { get; set; }

    [Column("timestamp")]
    public required DateTime Timestamp { get; set; }
}
