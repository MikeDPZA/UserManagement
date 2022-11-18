using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserManagement.Services.EventHandlers.JWT;

public class OnMessageReceivedHandler
{
    public static Task Handle(MessageReceivedContext ctx)
    {
        return Task.CompletedTask;
    }
}