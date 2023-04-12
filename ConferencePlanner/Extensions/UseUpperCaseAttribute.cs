using System.Reflection;
using HotChocolate.Types.Descriptors;

namespace ConferencePlanner.Extensions;

public class UseUpperCaseAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure
    (
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member
    )
    {
        descriptor.UseUpperCase();
    }
}
