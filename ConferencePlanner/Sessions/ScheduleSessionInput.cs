using ConferencePlanner.Data;

namespace ConferencePlanner.Sessions;

public record ScheduleSessionInput
{
    [ID(nameof(Session))] public int SessionId { get; set; }
    [ID(nameof(Track))] public int TrackId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}
