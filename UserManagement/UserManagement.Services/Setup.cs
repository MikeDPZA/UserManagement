using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Repository;
using UserManagement.Services.ControllerServices;
using UserManagement.Services.Interfaces;
using UserManagement.Services.MapperServices;

namespace UserManagement.Services;

public static class Setup
{
    public static IServiceCollection AddUserManagementServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);
        services.AddMappers();
        services.AddControllerServices();
        
        return services;
    }

    private static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
        services.AddScoped<IUserControllerService, UserControllerService>();
        return services;
    }
    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IUserMapperService, UserMapperService>();
        return services;
    }
}