using ConferencePlanner.Common;
using ConferencePlanner.Data;

namespace ConferencePlanner.Tracks;

public class AddTrackPayload : TrackPayloadBase
{
    public AddTrackPayload(Track track) : base(track)
    {
    }

    public AddTrackPayload(IReadOnlyList<UserError> errors) : base(errors)
    {
    }
}
