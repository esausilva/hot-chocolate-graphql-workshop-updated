using System.Reflection;
using ConferencePlanner.Data;
using HotChocolate.Types.Descriptors;

namespace ConferencePlanner.Extensions;

public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
{
    // Creates a so-called descriptor-attribute and allows us to wrap GraphQL
    // configuration code into attributes that you can apply to .NET type
    // system members
    protected override void OnConfigure
    (
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member
    )
    {
        descriptor.UseDbContext<ApplicationDbContext>();
    }
}
