namespace ConferencePlanner.Speakers;

public record AddSpeakerInput
(
    string Name,
    string? Bio,
    string? WebSite
);
