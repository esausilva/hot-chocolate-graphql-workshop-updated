using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Types;

public class AttendeeType : ObjectType<Attendee>
{
    protected override void Configure(IObjectTypeDescriptor<Attendee> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode(
                (ctx, id) => 
                    ctx.DataLoader<AttendeeByIdDataLoader>().LoadAsync(id, ctx.RequestAborted)
                );

        descriptor
            .Field(t => t.SessionsAttendees)
            .ResolveWith<AttendeeResolvers>(t => 
                t.GetSessionsAsync(default!, default!, default!, default))
            .UseDbContext<ApplicationDbContext>()
            .Name("sessions");
    }

    private class AttendeeResolvers
    {
        public async Task<IEnumerable<Session>> GetSessionsAsync
        (
            [Parent] Attendee attendee,
            ApplicationDbContext dbContext,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken
        )
        {
            int[] speakerIds = await dbContext.Attendees
                .Where(a => a.Id == attendee.Id)
                .Include(a => a.SessionsAttendees)
                .SelectMany(a => a.SessionsAttendees.Select(t => t.SessionId))
                .ToArrayAsync(cancellationToken);

            return await sessionById.LoadAsync(speakerIds, cancellationToken);
        }
    }
}
