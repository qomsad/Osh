using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.OshProgram.OshProgramAssignment;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining;

[Table("result_training")]
public class EmployeeResultTrainingModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("program_assignment_id")]
    public long OshProgramAssignmentId { get; set; }

    [ForeignKey(nameof(OshProgramAssignmentId))]
    public required OshProgramAssignmentModel OshProgramAssignment { get; set; }

    [Column("training_section_id")]
    public required long TrainingQuestionId { get; set; }

    [ForeignKey(nameof(TrainingQuestionId))]
    public required TrainingQuestionModel TrainingQuestion { get; set; }

    public required IEnumerable<EmployeeResultTrainingAnswerModel> Answers { get; set; }

    [Column("timestamp")]
    public required DateTime Timestamp { get; set; }
}
