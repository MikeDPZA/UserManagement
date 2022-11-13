using UserManagement.Common.Dto.Cognito;
using UserManagement.Common.Dto.Token;
using UserManagement.Common.Dto.User;

namespace UserManagement.Services.Interfaces;

public interface IIdentityService
{
    Task UpdateUser();
    Task DeleteUser();
    Task GetUser();
    Task ForgotPassword();
    Task<OAuthTokenResponse> Login(UserLoginDto loginDto);
    Task Logout();
    Task SignUp(RegisterUserDto registerDto);
}