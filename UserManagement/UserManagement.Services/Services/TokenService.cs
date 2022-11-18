using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services;

public class TokenService: ITokenService
{
    private readonly HttpContext? _context;
    public TokenService(IHttpContextAccessor contextAccessor)
    {
        _context = contextAccessor.HttpContext;
    }
    
    public JwtSecurityToken GetToken()
    {
        var authHeaderValues = _context?.Request.Headers[HeaderNames.Authorization].ToString()?.Replace("Bearer ", "");
        return new JwtSecurityToken(authHeaderValues);
    }

    public Guid GetTokenSubject()
    {
        var token = GetToken();
        
        return Guid.Parse(token.Subject);
    }
}