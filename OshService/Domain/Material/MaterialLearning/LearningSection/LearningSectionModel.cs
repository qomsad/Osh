using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OshService.Domain.Material.MaterialLearning.LearningSectionFile;
using OshService.Domain.OshProgram.OshProgram;

namespace OshService.Domain.Material.MaterialLearning.LearningSection;

[Table("learning_section")]
public class LearningSectionModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("name"), MaxLength(255)]
    public required string Name { get; set; }

    [Column("text")]
    public required string Text { get; set; }

    public LearningSectionFileModel? LearningSectionFile { get; set; }

    [Column("program_id")]
    public required long OshProgramId { get; set; }

    [ForeignKey(nameof(OshProgramId))]
    public OshProgramModel OshProgram { get; set; } = null!;
}
