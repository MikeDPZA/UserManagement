using Microsoft.AspNetCore.Authorization;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Api.Services;

[Authorize(Policy = "Token")]
public class CurrentUser
{
    public readonly Guid Id;
    public readonly string Firstname;
    public readonly string Lastname;
    public readonly string Email;
    public readonly string UserIdentifier;
    public readonly string[] Permissions;
    
    public CurrentUser(IPermissionRepository userRepository)
    {
        var currentUser = userRepository.GetUserPermissionDetails(Guid.Empty);
        Id = currentUser.UserId;
        Firstname = currentUser.Firstname;
        Lastname = currentUser.Lastname;
        Email = currentUser.Email;
        UserIdentifier = currentUser.UserIdentifier;
        Permissions = currentUser.Permissions;
    }
}