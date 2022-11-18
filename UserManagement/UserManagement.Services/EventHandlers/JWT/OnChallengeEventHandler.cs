using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserManagement.Services.EventHandlers.JWT;

public class OnChallengeEventHandler
{
    public static Task Handle(JwtBearerChallengeContext ctx)
    {
        return Task.CompletedTask;
    }
}