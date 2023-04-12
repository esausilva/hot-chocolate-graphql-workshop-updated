using ConferencePlanner.Attendees;
using ConferencePlanner.Data;
using ConferencePlanner.DataLoader;
using ConferencePlanner.Sessions;
using ConferencePlanner.Speakers;
using ConferencePlanner.Tracks;
using ConferencePlanner.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddPooledDbContextFactory<ApplicationDbContext>(options => 
        options.UseSqlite("Data Source=conferences.db")
    )
    .AddGraphQLServer()
    .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)
    
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<AttendeeQueries>()
    .AddTypeExtension<SessionQueries>()
    .AddTypeExtension<SpeakerQueries>()
    .AddTypeExtension<TrackQueries>()
    
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<AttendeeMutations>()
    .AddTypeExtension<SessionMutations>()
    .AddTypeExtension<SpeakerMutations>()
    .AddTypeExtension<TrackMutations>()
    
    .AddSubscriptionType(d => d.Name("Subscription"))
    .AddTypeExtension<AttendeeSubscriptions>()
    .AddTypeExtension<SessionSubscriptions>()
    
    .AddType<AttendeeType>()
    .AddType<AttendeeType>()
    .AddType<SessionType>()
    .AddType<SpeakerType>()
    .AddType<TrackType>()
    
    .AddGlobalObjectIdentification() // .EnableRelaySupport()
    
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions() // in-memory pub/sub system for GraphQL subscriptions to our schema
    
    .AddDataLoader<SpeakerByIdDataLoader>()
    .AddDataLoader<SessionByIdDataLoader>();

var app = builder.Build();

app.UseWebSockets();
app.MapGraphQL();
app.MapGet("/", () => "Hello World!");
app.Run();
