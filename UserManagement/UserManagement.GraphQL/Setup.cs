using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.GraphQL.Schema;
using UserManagement.Repository.Context;

namespace UserManagement.GraphQL;

public static class Setup
{
    public static IServiceCollection AddUserManagementGraph(this IServiceCollection services)
    {
        services.AddScoped<Query>();
        
        services.AddGraphQLServer()
            .RegisterDbContext<UserManagementContext>()
            .AddQueryType<Query>();
            // .AddMutationType<Mutation>();

        services.AddInMemorySubscriptions();
        
        
        return services;
    }

    public static IEndpointRouteBuilder UseUserManagementGraph(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGraphQL();
        return endpoint;
    }
}
