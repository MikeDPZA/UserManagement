using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Common.Dto.AppSettings;
using UserManagement.Repository;
using UserManagement.Services.ControllerServices;
using UserManagement.Services.EventHandlers.JWT;
using UserManagement.Services.FacadeServices;
using UserManagement.Services.IdentityServices;
using UserManagement.Services.Interfaces;
using UserManagement.Services.MapperServices;
using UserManagement.Services.Services;

namespace UserManagement.Services;

public static class Setup
{
    public static IServiceCollection AddUserManagementServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddRepository(configuration);
        services.AddMappers();
        services.AddControllerServices();
        services.AddFacadeServices();
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

                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = OnChallengeEventHandler.Handle,
                    OnForbidden = OnForbiddenEventHandler.Handle,
                    OnAuthenticationFailed = OnAuthenticationFailedHandler.Handle,
                    OnMessageReceived = OnMessageReceivedHandler.Handle,
                    OnTokenValidated = OnTokenValidatedHandler.Handle
                };
            });

        services.AddAuthorization(options =>
        {
            
        });
        
        services.AddScoped<IIdentityService, CognitoIdentityService>();

        return services;
    }

    private static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
        services.AddScoped<IUserControllerService, UserControllerService>();
        services.AddScoped<IPermissionControllerService, PermissionControllerService>();
        return services;
    }

    private static IServiceCollection AddFacadeServices(this IServiceCollection services)
    {
        services.AddScoped<IOAuthFacade, OAuthFacade>();

        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IUserMapperService, UserMapperService>();
        services.AddScoped<IPermissionMapperService, PermissionMapperService>();
        return services;
    }
}