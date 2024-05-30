using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OshService.Domain.Material.MaterialLearning.LearningSection;

namespace OshService.Domain.Material.MaterialLearning.LearningSectionFile;

[Table("learning_section_file")]
public class LearningSectionFileModel
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required long Id { get; set; }

    [Column("file_path")]
    public required string FilePath { get; set; }

    [Column("file_type")]
    public required LearningSectionFileType FileType { get; set; }

    [Column("learning_section_id")]
    public required long LearningSectionId { get; set; }

    [ForeignKey(nameof(LearningSectionId))]
    public required LearningSectionModel LearningSection { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LearningSectionFileType
{
    Pdf,
}
