using Microsoft.AspNetCore.Builder;
using UserManagement.Services.Middleware;

namespace UserManagement.Services.Extensions;

public static class PermissionMiddlewareExtension
{
    public static IApplicationBuilder UsePermissionMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<PermissionMiddleware>();
}