using ConferencePlanner.Common;
using ConferencePlanner.Data;

namespace ConferencePlanner.Attendees;

public class AttendeePayloadBase : Payload
{
    public Attendee? Attendee { get; }

    protected AttendeePayloadBase(Attendee attendee)
    {
        Attendee = attendee;
    }

    protected AttendeePayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
}
