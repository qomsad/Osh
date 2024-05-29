using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;

namespace OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;

[Table("training_question_answer")]
public class TrainingQuestionAnswerModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("index")]
    public required int Index { get; set; }

    [Column("value")]
    public required string Value { get; set; }

    [Column("is_right")]
    public required bool IsRight { get; set; }

    [Column("training_question_id")]
    public required long TrainingQuestionId { get; set; }

    [ForeignKey(nameof(TrainingQuestionId))]
    public required TrainingQuestionModel TrainingQuestion { get; set; }
}
