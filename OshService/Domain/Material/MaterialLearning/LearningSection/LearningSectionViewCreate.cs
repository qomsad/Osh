using OshService.Domain.Material.MaterialLearning.LearningSectionFile;

namespace OshService.Domain.Material.MaterialLearning.LearningSection;

public class LearningSectionViewCreate
{
    public required string Name { get; set; }

    public required string Text { get; set; }

    public LearningSectionFileViewCreate? LearningSectionFile { get; set; }
}
