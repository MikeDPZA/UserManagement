using Microsoft.AspNetCore.Authorization;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services;

[Authorize]
public class CurrentUser: ICurrentUser
{
    public Guid Id { get; }
    public string Firstname { get; }
    public string Lastname { get; }
    public string Email { get; }
    public string UserIdentifier { get; }
    public string[] Permissions { get; }
    
    public CurrentUser(IPermissionRepository userRepository, ITokenService tokenService)
    {
        // var user = tokenService.GetTokenSubject();
        //
        // var currentUser = userRepository.GetUserPermissionDetails(user);
        // Id = currentUser.UserId;
        // Firstname = currentUser.Firstname;
        // Lastname = currentUser.Lastname;
        // Email = currentUser.Email;
        // UserIdentifier = currentUser.UserIdentifier;
        // Permissions = currentUser.Permissions;
    }
}

public interface ICurrentUser
{
    Guid Id { get; }
    string Firstname {get; }
    string Lastname {get; }
    string Email {get; }
    string UserIdentifier {get; }
    string[] Permissions {get; }
}