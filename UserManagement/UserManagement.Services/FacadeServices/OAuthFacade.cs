using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UserManagement.Common.Dto.Token;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.FacadeServices;

public class OAuthFacade : IOAuthFacade
{
    public OAuthFacade()
    {
    }

    public async Task<OAuthTokenResponse> GetUserToken(string code, string clientId, string clientSecret, string domain, string redirectUrl)
    {
        var client = new HttpClient();
        var form = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "redirect_uri", redirectUrl },
            { "code", code },
        };

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
        var result = await client.PostAsync($"https://{domain}/oauth2/token", new FormUrlEncodedContent(form));

        var content = await result.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<OAuthTokenResponse>(content) ?? new OAuthTokenResponse();
    }
}