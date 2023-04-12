using ConferencePlanner.Data;

namespace ConferencePlanner.Sessions;

public record AddSessionInput
{
    public string Title { get; set; }
    public string? Abstract { get; set; }
    [ID(nameof(Speaker))] public IReadOnlyList<int> SpeakerIds { get; set; }   
}
