using System.IdentityModel.Tokens.Jwt;

namespace UserManagement.Services.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GetToken();
    Guid GetTokenSubject();
}