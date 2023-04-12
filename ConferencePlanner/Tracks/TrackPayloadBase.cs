using ConferencePlanner.Common;
using ConferencePlanner.Data;

namespace ConferencePlanner.Tracks;

public class TrackPayloadBase : Payload
{
    public TrackPayloadBase(Track track)
    {
        Track = track;
    }

    public TrackPayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }

    public Track? Track { get; }
}
