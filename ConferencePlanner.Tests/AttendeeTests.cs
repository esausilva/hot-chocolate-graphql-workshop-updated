using ConferencePlanner.Attendees;
using ConferencePlanner.Data;
using ConferencePlanner.Types;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;

namespace ConferencePlanner.Tests;

public class AttendeeTests
{
    [Fact]
    public async Task Attendee_Schema_Changed()
    {
        // arrange
        // act
        ISchema schema = await new ServiceCollection()
            .AddPooledDbContextFactory<ApplicationDbContext>(
                options => options.UseInMemoryDatabase("Data Source=conferences.db")
            )
            .AddGraphQL()
            .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)

            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<AttendeeQueries>()
            
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<AttendeeMutations>()
            
            .AddType<AttendeeType>()
            .AddType<SessionType>()
            .AddType<SpeakerType>()
            .AddType<TrackType>()

            .AddGlobalObjectIdentification() // .EnableRelaySupport()

            .BuildSchemaAsync();
            
        // assert
        schema.Print().MatchSnapshot();
    }
    
    [Fact]
    public async Task RegisterAttendee()
    {
        // arrange
        IRequestExecutor executor = await new ServiceCollection()
            .AddPooledDbContextFactory<ApplicationDbContext>(
                options => options.UseInMemoryDatabase("Data Source=conferences.db")
            )
            .AddGraphQL()
            .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)

            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<AttendeeQueries>()
            
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<AttendeeMutations>()
            
            .AddType<AttendeeType>()
            .AddType<SessionType>()
            .AddType<SpeakerType>()
            .AddType<TrackType>()

            .AddGlobalObjectIdentification() // .EnableRelaySupport()

            .BuildRequestExecutorAsync();

        // act
        IExecutionResult result = await executor.ExecuteAsync(@"
        mutation RegisterAttendee {
            registerAttendee(
                input: {
                    emailAddress: ""michael@chillicream.com""
                        firstName: ""michael""
                        lastName: ""staib""
                        userName: ""michael3""
                    }
            )
            {
                attendee {
                    id
                }
            }
        }");

        // assert
        result.ToJson().MatchSnapshot();
    }
}
