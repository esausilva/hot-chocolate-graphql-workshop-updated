using ConferencePlanner.Data;

namespace ConferencePlanner.Attendees;

public record CheckInAttendeeInput
{
    [ID(nameof(Session))] public int  SessionId { get; set; }
    [ID(nameof(Attendee))] public int AttendeeId { get; set; }
}
