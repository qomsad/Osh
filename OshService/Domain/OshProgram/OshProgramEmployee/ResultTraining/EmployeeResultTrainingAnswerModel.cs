using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining;

[Table("result_training_answer")]
public class EmployeeResultTrainingAnswerModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("program_status_training_id")]
    public required long OshProgramStatusTrainingId { get; set; }

    [ForeignKey(nameof(OshProgramStatusTrainingId))]
    public required EmployeeResultTrainingModel StatusTraining { get; set; }

    [Column("training_question_answer_id")]
    public required long TrainingQuestionAnswerId { get; set; }

    [ForeignKey(nameof(TrainingQuestionAnswerId))]
    public required TrainingQuestionAnswerModel ActualAnswer { get; set; }
}
