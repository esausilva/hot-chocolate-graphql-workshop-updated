using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Extensions;

public static class ObjectFieldDescriptorExtensions
{
    // The UseDbContext will create a new middleware that handles scoping for a field
    public static IObjectFieldDescriptor UseDbContext<TDbContext>(this IObjectFieldDescriptor descriptor) 
        where TDbContext : DbContext
    {
        return descriptor.UseScopedService<TDbContext>(
            // Will rent from the pool a DBContext
            create: s => s.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),

            // Will return it after the middleware is finished
            disposeAsync: (s, c) => c.DisposeAsync()
        );
    }
    
    public static IObjectFieldDescriptor UseUpperCase(this IObjectFieldDescriptor descriptor)
    {
        return descriptor.Use(next => async context =>
        {
            await next(context);

            if (context.Result is string s)
            {
                context.Result = s.ToUpperInvariant();
            }
        });
    }
}
