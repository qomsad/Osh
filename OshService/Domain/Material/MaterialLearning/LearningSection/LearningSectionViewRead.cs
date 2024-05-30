using OshService.Domain.Material.MaterialLearning.LearningSectionFile;

namespace OshService.Domain.Material.MaterialLearning.LearningSection;

public class LearningSectionViewRead
{
    public required long Id { get; set; }

    public required string Name { get; set; }

    public required string Text { get; set; }

    public LearningSectionFileViewRead? LearningSectionFile { get; set; }
}
