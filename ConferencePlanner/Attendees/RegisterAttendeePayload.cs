using ConferencePlanner.Common;
using ConferencePlanner.Data;

namespace ConferencePlanner.Attendees;

public class RegisterAttendeePayload : AttendeePayloadBase
{
    public RegisterAttendeePayload(Attendee attendee)
        : base(attendee)
    {
    }

    public RegisterAttendeePayload(UserError error)
        : base(new[] { error })
    {
    }
}
