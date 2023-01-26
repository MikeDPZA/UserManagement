using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;
using UserManagement.Repository.Repos;

namespace UserManagement.Repository;

public static class Setup
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("UserManagement");    
        services.AddDbContext<UserManagementContext>(_ => _.UseNpgsql(connectionString).UseLazyLoadingProxies());
        services.AddDependencies();
        return services;
    }

    private static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        
        return services;
    }
}