using ConferencePlanner.Common;
using ConferencePlanner.Data;

namespace ConferencePlanner.Tracks;

public class RenameTrackPayload : TrackPayloadBase
{
    public RenameTrackPayload(Track track) : base(track)
    {
    }

    public RenameTrackPayload(IReadOnlyList<UserError> errors) : base(errors)
    {
    }
}
