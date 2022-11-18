using UserManagement.Services.Services;

namespace UserManagement.Api.Extensions;

public static class CurrentUserExtensions
{
    public static bool HasPermission(this ICurrentUser user, string permission)
        => user.Permissions.Any(_ => _.ToLower() == permission.ToLower());
    
    public static bool HasPermission(this ICurrentUser user, params string[] permission)
        => user.Permissions.Any(permission.Contains);
}