using ConferencePlanner.Data;
using ConferencePlanner.Extensions;

namespace ConferencePlanner.Speakers;

[ExtendObjectType("Mutation")]
public class SpeakerMutations
{
    [UseApplicationDbContext]
    public async Task<AddSpeakerPayload> AddSpeakerAsync
    (
        AddSpeakerInput input,
        ApplicationDbContext context
    )
    {
        var (name, bio, webSite) = input;
        var speaker = new Speaker
        {
            Name = name,
            Bio = bio,
            WebSite = webSite
        };

        context.Speakers.Add(speaker);
        await context.SaveChangesAsync();

        return new AddSpeakerPayload(speaker);
    }
}
