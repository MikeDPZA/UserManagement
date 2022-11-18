using UserManagement.Common.Dto.Token;

namespace UserManagement.Services.Interfaces;

public interface IOAuthFacade
{
    Task<OAuthTokenResponse> GetUserToken(string code, string clientId, string clientSecret, string domain, string redirectUrl);
}