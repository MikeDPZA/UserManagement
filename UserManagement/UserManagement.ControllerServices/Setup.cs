using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.ControllerServices.Interfaces;
using UserManagement.ControllerServices.Services;
using UserManagement.Repository;

namespace UserManagement.ControllerServices;

public static class Setup
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);

        services.AddScoped<IUserControllerService, UserControllerService>();
        
        return services;
    }
}