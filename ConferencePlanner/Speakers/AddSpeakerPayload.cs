using ConferencePlanner.Common;
using ConferencePlanner.Data;

namespace ConferencePlanner.Speakers;

public class AddSpeakerPayload : SpeakerPayloadBase
{
    public Speaker Speaker { get; }

    public AddSpeakerPayload(Speaker speaker)
        : base(speaker)
    {
        Speaker = speaker;
    }
    
    public AddSpeakerPayload(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
}
