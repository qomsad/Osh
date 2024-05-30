namespace OshService.Domain.Material.MaterialLearning.LearningSectionFile;

public class LearningSectionFileViewCreate
{
    public required string FilePath { get; set; }

    public required LearningSectionFileType FileType { get; set; }
}
