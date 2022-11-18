using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserManagement.Services.EventHandlers.JWT;

public class OnTokenValidatedHandler
{
    public static Task Handle(TokenValidatedContext ctx)
    {
        return Task.CompletedTask;
    }
}