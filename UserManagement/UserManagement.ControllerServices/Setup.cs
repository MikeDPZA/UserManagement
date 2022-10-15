using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Repository;

namespace UserManagement.ControllerServices;

public static class Setup
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);
        return services;
    }
}