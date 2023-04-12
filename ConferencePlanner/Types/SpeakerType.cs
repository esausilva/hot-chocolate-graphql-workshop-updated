using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Types;

// In the type configuration we are giving SessionSpeakers a new name sessions. Also, we
// are binding a new resolver to this field which also rewrites the result type. The new
// field sessions now returns [Session].
/*
 It will rename sessionSpeakers => sessions 
type Speaker {
    sessionSpeakers: [SessionSpeaker!]! => sessions: [Session!]!
    id: Int!
    name: String!
    bio: String
    website: String
}
 */
public class SpeakerType : ObjectType<Speaker>
{
    protected override void Configure(IObjectTypeDescriptor<Speaker> descriptor)
    {
        // The following piece of code marked our SpeakerType as implementing the Node interface.
        // It also defined that the id field that the node interface specifies is implemented by
        // the Id on our entity. The internal Id is consequently rewritten to a global object
        // identifier that contains the internal id plus the type name. Last but not least we
        // defined a ResolveNode that is able to load the entity by id.
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode(
                (ctx, id) => 
                    ctx.DataLoader<SpeakerByIdDataLoader>().LoadAsync(id, ctx.RequestAborted)
               );

        descriptor
            .Field(t => t.SessionSpeakers)
            .ResolveWith<SpeakerResolvers>(t =>
                t.GetSessionsAsync(default!, default!, default!, default))
            .UseDbContext<ApplicationDbContext>()
            .Name("sessions");
    }

    private class SpeakerResolvers
    {
        public async Task<IEnumerable<Session?>> GetSessionsAsync
        (
            [Parent] Speaker speaker,
            ApplicationDbContext dbContext,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken
        )
        {
            int[] sessionIds = await dbContext.Speakers
                .Where(s => s.Id == speaker.Id)
                .Include(s => s.SessionSpeakers)
                .SelectMany(s => s.SessionSpeakers.Select(t => t.SessionId))
                .ToArrayAsync(cancellationToken);

            return await sessionById.LoadAsync(sessionIds, cancellationToken);
        }
    }
}