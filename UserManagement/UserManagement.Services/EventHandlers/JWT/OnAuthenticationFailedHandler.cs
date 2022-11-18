using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserManagement.Services.EventHandlers.JWT;

public class OnAuthenticationFailedHandler
{
    public static Task Handle(AuthenticationFailedContext ctx)
    {
        return Task.CompletedTask;
    }
}