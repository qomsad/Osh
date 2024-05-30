namespace OshService.Domain.Specialty;

public class SpecialtyViewRead
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public required DateTime Created { get; set; }

    public required DateTime Updated { get; set; }
}
