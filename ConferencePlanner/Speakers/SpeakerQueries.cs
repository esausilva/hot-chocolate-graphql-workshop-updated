using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;

namespace ConferencePlanner.Speakers;

[ExtendObjectType("Query")]
public class SpeakerQueries
{
    // By annotating UseApplicationDbContext we are essentially applying a Middleware
    // to the field resolver pipeline
    // [UseApplicationDbContext]
    // public Task<List<Speaker>> GetSpeakers(ApplicationDbContext context) =>
    //     // Executing the IQueryable by using ToListAsync
    //     context.Speakers.ToListAsync();
    [UsePaging]
    public IQueryable<Speaker> GetSpeakers(ApplicationDbContext context) =>
        context.Speakers.OrderBy(t => t.Name);
    
    public Task<Speaker?> GetSpeakerByIdAsync
    (
        [ID(nameof(Speaker))] int id,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken
    ) => dataLoader.LoadAsync(id, cancellationToken);
    
    public async Task<IEnumerable<Speaker?>> GetSpeakersByIdAsync
    (
        [ID(nameof(Speaker))] int[] ids,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken
    ) => await dataLoader.LoadAsync(ids, cancellationToken);
}
