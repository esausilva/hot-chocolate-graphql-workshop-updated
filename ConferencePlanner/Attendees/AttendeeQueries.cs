using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;
using ConferencePlanner.Extensions;

namespace ConferencePlanner.Attendees;

[ExtendObjectType(Name = "Query")]
public class AttendeeQueries
{
    [UseApplicationDbContext]
    [UsePaging]
    public IQueryable<Attendee> GetAttendees(ApplicationDbContext context) =>
        context.Attendees;

    public Task<Attendee> GetAttendeeByIdAsync
    (
        [ID(nameof(Attendee))] int id,
        AttendeeByIdDataLoader attendeeById,
        CancellationToken cancellationToken
    ) => attendeeById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Attendee>> GetAttendeesByIdAsync
    (
        [ID(nameof(Attendee))] int[] ids,
        AttendeeByIdDataLoader attendeeById,
        CancellationToken cancellationToken
    ) => await attendeeById.LoadAsync(ids, cancellationToken);
}
