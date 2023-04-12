using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;
using ConferencePlanner.Types;

namespace ConferencePlanner.Sessions;

[ExtendObjectType("Query")]
public class SessionQueries
{
    // [UseApplicationDbContext]
    // public async Task<IEnumerable<Session>> GetSessionsAsync
    // (
    //     ApplicationDbContext context,
    //     CancellationToken cancellationToken
    // ) => await context.Sessions.ToListAsync(cancellationToken);
    [UsePaging(typeof(NonNullType<SessionType>))]
    [UseFiltering(typeof(SessionFilterInputType))]
    [UseSorting]
    public IQueryable<Session> GetSessions(ApplicationDbContext context) =>
        context.Sessions;

    public Task<Session?> GetSessionByIdAsync
    (
        [ID(nameof(Session))] int id,
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken
    ) => sessionById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Session?>> GetSessionsByIdAsync
    (
        [ID(nameof(Session))] int[] ids,
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken
    ) => await sessionById.LoadAsync(ids, cancellationToken);
}
