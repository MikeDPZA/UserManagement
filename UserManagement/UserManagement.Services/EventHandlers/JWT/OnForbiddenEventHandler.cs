using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserManagement.Services.EventHandlers.JWT;

public class OnForbiddenEventHandler
{
    public static Task Handle(ForbiddenContext ctx)
    {
        return Task.CompletedTask;
    }
}