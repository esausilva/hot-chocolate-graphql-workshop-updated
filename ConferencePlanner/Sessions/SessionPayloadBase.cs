using ConferencePlanner.Common;
using ConferencePlanner.Data;

namespace ConferencePlanner.Sessions;

public class SessionPayloadBase : Payload
{
    protected SessionPayloadBase(Session session)
    {
        Session = session;
    }

    protected SessionPayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }

    public Session? Session { get; }
}
