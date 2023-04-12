using ConferencePlanner.Data;
using ConferencePlanner.Extensions;

namespace ConferencePlanner.Tracks;

[ExtendObjectType("Mutation")]
public class TrackMutations
{
    [UseApplicationDbContext]
    public async Task<AddTrackPayload> AddTrackAsync
    (
        AddTrackInput input,
        ApplicationDbContext context,
        CancellationToken cancellationToken
    )
    {
        var track = new Track { Name = input.Name };
        context.Tracks.Add(track);

        await context.SaveChangesAsync(cancellationToken);

        return new AddTrackPayload(track);
    }
    
    [UseApplicationDbContext]
    public async Task<RenameTrackPayload> RenameTrackAsync
    (
        RenameTrackInput input,
        ApplicationDbContext context,
        CancellationToken cancellationToken
    )
    {
        Track track = await context.Tracks.FindAsync(input.Id);
        track.Name = input.Name;

        await context.SaveChangesAsync(cancellationToken);

        return new RenameTrackPayload(track);
    }
}
