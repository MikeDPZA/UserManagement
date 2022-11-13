using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Common.Dto.AppSettings;
using UserManagement.Repository;
using UserManagement.Services.ControllerServices;
using UserManagement.Services.IdentityServices;
using UserManagement.Services.Interfaces;
using UserManagement.Services.MapperServices;

namespace UserManagement.Services;

public static class Setup
{
    public static IServiceCollection AddUserManagementServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRepository(configuration);
        services.AddMappers();
        services.AddControllerServices();
        services.AddCognito(configuration);
        return services;
    }

    private static IServiceCollection AddCognito(this IServiceCollection services, IConfiguration configuration)
    {
        
        var awsConfig = configuration.GetSection("Aws").Get<AwsAppSettings>();
        services.Configure<AwsAppSettings>(configuration.GetSection("Aws"));

        services.AddCognitoIdentity();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var cognitoUrl = $"https://cognito-idp.{awsConfig.Region}.amazonaws.com/{awsConfig.UserPoolId}";
                options.Authority = cognitoUrl;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = cognitoUrl,
                    ValidateLifetime = true,
                    LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
                    ValidateAudience = false
                };
            });
        services.AddScoped<IIdentityService, CognitoIdentityService>();
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