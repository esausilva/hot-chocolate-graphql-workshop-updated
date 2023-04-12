using ConferencePlanner.Data;
using HotChocolate.Data.Filters;

namespace ConferencePlanner.Sessions;

public class SessionFilterInputType : FilterInputType<Session>
{
    protected override void Configure(IFilterInputTypeDescriptor<Session> descriptor)
    {
        descriptor.Ignore(t => t.Id);
        descriptor.Ignore(t => t.TrackId);
    }
}
