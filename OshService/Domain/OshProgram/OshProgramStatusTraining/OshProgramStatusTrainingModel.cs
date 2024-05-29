using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.OshProgram.OshProgramStatusTrainingAnswer;

namespace OshService.Domain.OshProgram.OshProgramStatusTraining;

[Table("program_status_training")]
public class OshProgramStatusTrainingModel
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

    [Column("training_status")]
    public required OshProgramTrainingStatus TrainingStatus { get; set; }

    public required IEnumerable<OshProgramStatusTrainingAnswerModel> Answers { get; set; }

    [Column("timestamp")]
    public required DateTime Timestamp { get; set; }
}

public enum OshProgramTrainingStatus
{
    ReadQuestion,
    AnswerQuestion,
}
