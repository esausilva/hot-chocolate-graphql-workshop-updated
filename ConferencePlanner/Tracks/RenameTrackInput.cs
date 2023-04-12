using ConferencePlanner.Data;

namespace ConferencePlanner.Tracks;

public record RenameTrackInput
{
    [ID(nameof(Track))] public int Id { get; set; }
    public string Name { get; set; }
}
