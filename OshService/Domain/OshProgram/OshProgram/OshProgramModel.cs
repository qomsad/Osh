using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.Organization;
using OshService.Domain.Specialty;

namespace OshService.Domain.OshProgram.OshProgram;

[Table("program")]
public class OshProgramModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("organization_id")]
    public required long OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public OrganizationModel Organization { get; set; } = null!;

    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public required string Description { get; set; }

    public required IEnumerable<LearningSectionModel> LearningSections { get; set; }

    public required IEnumerable<TrainingQuestionModel> TrainingQuestions { get; set; }

    [Column("learning_minutes_duration")]
    public required int LearningMinutesDuration { get; set; }

    [Column("training_minutes_duration")]
    public required int TrainingMinutesDuration { get; set; }

    [Column("speciality_id")]
    public long? SpecialityId { get; set; }

    [ForeignKey(nameof(SpecialityId))]
    public SpecialtyModel? Specialty { get; set; }

    [Column("auto_assignment_type")]
    public required OshProgramAutoAssignment AutoAssignmentType { get; set; } = OshProgramAutoAssignment.FullManual;

    [Column("max_auto_assignments")]
    public int? MaxAutoAssignments { get; set; }

    [Column("training_success_rate")]
    public int TrainingSuccessRate { get; set; } = 0;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OshProgramAutoAssignment
{
    FullManual,
    FirstManual,
    BySpeciality,
    All,
}
