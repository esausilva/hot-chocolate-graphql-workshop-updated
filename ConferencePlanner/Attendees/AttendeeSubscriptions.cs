using ConferencePlanner.Data;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace ConferencePlanner.Attendees;

[ExtendObjectType(Name = "Subscription")]
public class AttendeeSubscriptions
{
    [Subscribe(With = nameof(SubscribeToOnAttendeeCheckedInAsync))]
    public SessionAttendeeCheckIn OnAttendeeCheckedIn
    (
        [ID(nameof(Session))] int sessionId,
        [EventMessage] int attendeeId
    ) => new SessionAttendeeCheckIn(attendeeId, sessionId);

    public async ValueTask<ISourceStream<int>> SubscribeToOnAttendeeCheckedInAsync
    (
        int sessionId,
        [Service] ITopicEventReceiver eventReceiver,
        CancellationToken cancellationToken
    ) => await eventReceiver.SubscribeAsync<int>(
            $"OnAttendeeCheckedIn_{sessionId}", cancellationToken
        );
}
