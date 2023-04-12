using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;
using ConferencePlanner.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Tracks;

[ExtendObjectType("Query")]
public class TrackQueries
{
    // [UseApplicationDbContext]
    // public async Task<IEnumerable<Track>> GetTracksAsync
    // (
    //     ApplicationDbContext context,
    //     CancellationToken cancellationToken
    // ) => await context.Tracks.ToListAsync(cancellationToken);
    
    // The new resolver will instead of executing the database query return an IQueryable. The IQueryable
    // is like a query builder. By applying the UsePaging middleware, we are rewriting the database query
    // to only fetch the items that we need for our data-set.
    [UsePaging]
    public IQueryable<Track> GetTracks(ApplicationDbContext context) =>
        context.Tracks.OrderBy(t => t.Name);

    [UseApplicationDbContext]
    public async Task<Track?> GetTrackByNameAsync
    (
        string name,
        ApplicationDbContext context,
        CancellationToken cancellationToken
    ) => await context.Tracks.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);

    [UseApplicationDbContext]
    public async Task<IEnumerable<Track?>> GetTrackByNamesAsync
    (
        string[] names,
        ApplicationDbContext context,
        CancellationToken cancellationToken
    ) => await context.Tracks.Where(t => names.Contains(t.Name)).ToListAsync(cancellationToken);

    public Task<Track?> GetTrackByIdAsync
    (
        [ID(nameof(Track))] int id,
        TrackByIdDataLoader trackById,
        CancellationToken cancellationToken
    ) => trackById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Track?>> GetTracksByIdAsync
    (
        [ID(nameof(Track))] int[] ids,
        TrackByIdDataLoader trackById,
        CancellationToken cancellationToken
    ) => await trackById.LoadAsync(ids, cancellationToken);
}
