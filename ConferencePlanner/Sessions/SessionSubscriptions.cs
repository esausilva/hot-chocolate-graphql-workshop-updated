using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;

namespace ConferencePlanner.Sessions;

[ExtendObjectType(Name = "Subscription")]
public class SessionSubscriptions
{
    /**
     * The [Topic] attribute can be put on the method or a parameter of the method and will
     * infer the pub/sub-topic for this subscription.
     *
     * The [Subscribe] attribute tells the schema builder that this resolver method needs to
     * be hooked up to the pub/sub-system. This means that in the background, the resolver
     * compiler will create a so-called subscribe resolver that handles subscribing to the
     * pub/sub-system.
     *
     * The [EventMessage] attribute marks the parameter where the execution engine shall
     * inject the message payload of the pub/sub-system.
     */
    [Subscribe]
    [Topic]
    public Task<Session?> OnSessionScheduledAsync
    (
        [EventMessage] int sessionId,
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken
    ) => sessionById.LoadAsync(sessionId, cancellationToken);
}
