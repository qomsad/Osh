namespace OshService.Domain.Material.MaterialLearning.LearningSectionFile;

public class LearningSectionFileViewRead
{
    public required long Id { get; set; }

    public required string FilePath { get; set; }

    public required LearningSectionFileType FileType { get; set; }
}
