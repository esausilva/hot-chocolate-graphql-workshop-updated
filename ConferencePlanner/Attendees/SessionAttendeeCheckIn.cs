using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;
using ConferencePlanner.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Attendees;

public class SessionAttendeeCheckIn
{
    [ID(nameof(Attendee))] private int AttendeeId { get; }
    [ID(nameof(Session))] private int SessionId { get; }
    
    public SessionAttendeeCheckIn(int attendeeId, int sessionId)
    {
        AttendeeId = attendeeId;
        SessionId = sessionId;
    }

    [UseApplicationDbContext]
    public async Task<int> CheckInCountAsync
    (
        ApplicationDbContext context,
        CancellationToken cancellationToken
    ) => await context.Sessions
            .Where(session => session.Id == SessionId)
            .SelectMany(session => session.SessionAttendees)
            .CountAsync(cancellationToken);

    public Task<Attendee> GetAttendeeAsync
    (
        AttendeeByIdDataLoader attendeeById,
        CancellationToken cancellationToken
    ) => attendeeById.LoadAsync(AttendeeId, cancellationToken);

    public Task<Session?> GetSessionAsync
    (
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken
    ) => sessionById.LoadAsync(SessionId, cancellationToken);
}
