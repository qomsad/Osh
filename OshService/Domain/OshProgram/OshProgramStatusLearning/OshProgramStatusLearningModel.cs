using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.OshProgram.OshProgramAssignment;

namespace OshService.Domain.OshProgram.OshProgramStatusLearning;

[Table("program_status_learning")]
public class OshProgramStatusLearningModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("program_assignment_id")]
    public long OshProgramAssignmentId { get; set; }

    [ForeignKey(nameof(OshProgramAssignmentId))]
    public required OshProgramAssignmentModel OshProgramAssignment { get; set; }

    [Column("learning_section_id")]
    public required long LearningSectionId { get; set; }

    [ForeignKey(nameof(LearningSectionId))]
    public required LearningSectionModel LearningSection { get; set; }

    [Column("learning_status")]
    public required OshProgramLearningStatus LearningStatus { get; set; }

    [Column("timestamp")]
    public required DateTime Timestamp { get; set; }
}

public enum OshProgramLearningStatus
{
    OpenSection,
    CloseSection,
}
