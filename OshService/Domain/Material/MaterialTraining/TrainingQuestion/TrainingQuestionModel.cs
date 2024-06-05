using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;
using OshService.Domain.OshProgram.OshProgram;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestion;

[Table("training_question")]
public class TrainingQuestionModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("index")]
    public required int Index { get; set; }

    [Column("type")]
    public TrainingQuestionType QuestionType { get; set; }

    [Column("question")]
    public required string Question { get; set; }

    [Column("rate")]
    public required int Rate { get; set; }

    public required IEnumerable<TrainingQuestionAnswerModel> Answers { get; set; }

    [Column("program_id")]
    public required long OshProgramId { get; set; }

    [ForeignKey(nameof(OshProgramId))]
    public required OshProgramModel OshProgram { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TrainingQuestionType
{
    QuestionMultiple,
    QuestionSingle,
}
