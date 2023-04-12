using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Types;

public class TrackType : ObjectType<Track>
{
    protected override void Configure(IObjectTypeDescriptor<Track> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) =>
                ctx.DataLoader<TrackByIdDataLoader>().LoadAsync(id, ctx.RequestAborted)
            );
                    
        descriptor
            .Field(t => t.Sessions)
            .ResolveWith<TrackResolvers>(t => 
                t.GetSessionsAsync(default!, default!, default!, default)
            )
            .UseDbContext<ApplicationDbContext>()
            .UsePaging<NonNullType<SessionType>>() // *
            .Name("sessions");
    }
    
    // * There is one caveat in our implementation with the TrackType. Since, we are using a DataLoader
    // within our resolver and first fetch the list of IDs we essentially will always fetch everything
    // and chop in memory. In an actual project this can be split into two actions by moving the DataLoader
    // part into a middleware and first page on the id queryable. Also one could implement a special
    // IPagingHandler that uses the DataLoader and applies paging logic.

    private class TrackResolvers
    {
        public async Task<IEnumerable<Session?>> GetSessionsAsync
        (
            [Parent] Track track,
            ApplicationDbContext dbContext,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken
        )
        {
            int[] sessionIds = await dbContext.Sessions
                .Where(s => s.Id == track.Id)
                .Select(s => s.Id)
                .ToArrayAsync(cancellationToken);

            return await sessionById.LoadAsync(sessionIds, cancellationToken);
        }
    }
}
