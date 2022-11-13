using UserManagement.Api.Middleware;

namespace UserManagement.Api.Extensions;

public static class PermissionMiddlewareExtension
{
    public static IApplicationBuilder UsePermissionMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<PermissionMiddleware>();
}