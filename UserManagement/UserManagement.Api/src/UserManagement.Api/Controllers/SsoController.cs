using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserManagement.Api.Services;
using UserManagement.Common.Dto.AppSettings;
using UserManagement.Common.Dto.Cognito;
using UserManagement.Common.Dto.Token;
using UserManagement.Common.Dto.User;
using UserManagement.Services.Interfaces;

namespace UserManagement.Api.Controllers;

/// <summary>
/// Controller for SSO actions
/// </summary>
[ApiController]
[Route("api/UserManagement/v1/SSO")]
public class SsoController: BaseController
{
    private readonly IIdentityService _identityService;
    private ILogger<SsoController> _logger;
    private readonly AwsAppSettings _awsSettings;
    private readonly IUserControllerService _userControllerService;

    
    /* This is the constructor for the SsoController class. It is initializing the class variables. */
    public SsoController(CurrentUser currentUser,
        IIdentityService identityService, 
        ILogger<SsoController> logger, 
        IOptions<AwsAppSettings> awsSettings, 
        IUserControllerService userControllerService
    ) : base(currentUser)
    {
        _identityService = identityService;
        _logger = logger;
        _userControllerService = userControllerService;
        _awsSettings = awsSettings.Value;
    }

    /// <summary>
    /// Logs a user in and returns a token
    /// </summary>
    /// <param name="login">This is a class that contains the username and password that the user will enter.</param>
    /// <returns>
    /// A JWT token
    /// </returns>
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] UserLoginDto login)
    {
        try
        {
            var result = _identityService.Login(login);
            return Ok(await result);
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to log in: {E}", e);
            return BadRequest(e.Message);
        }
    }
    
    
    /// <summary>
    /// Register a user in the Identity Provider
    /// </summary>
    /// <param name="registerUserDto">Users' details for signup</param>
    /// <returns>
    /// Ok()
    /// </returns>
    [HttpPost("SignUp")]
    public IActionResult SignUp([FromBody] RegisterUserDto registerUserDto)
    {
        try
        {
            _identityService.SignUp(registerUserDto);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to log in: {E}", e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("Login")]
    public async Task<IActionResult> Login([FromQuery] string code)
    {
        var client = new HttpClient();
        var form = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "client_id", _awsSettings.AppClientId },
            { "client_secret", _awsSettings.AppClientSecret },
            { "redirect_uri", _awsSettings.RedirectUrl },
            { "code", code },
        };
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_awsSettings.AppClientId}:{_awsSettings.AppClientSecret}")));
        var result = await client.PostAsync($"https://{_awsSettings.Domain}/oauth2/token", new FormUrlEncodedContent(form));

        var content = await result.Content.ReadAsStringAsync();
        
        if (!result.IsSuccessStatusCode)
        {
            return BadRequest(JsonSerializer.Deserialize<object>(content));
        }

        var token = JsonSerializer.Deserialize<OAuthTokenResponse>(content);
        var jwt = new JwtSecurityToken(token.AccessToken);

        var user = _userControllerService.GetUser(Guid.Parse(jwt.Subject));

        return RedirectAfterAuth(user, token);
    }

    private IActionResult RedirectAfterAuth(User user, OAuthTokenResponse token )
    {
        var returnUrl = new Uri(_awsSettings.RedirectUrl.Replace("api/UserManagement/v1/SSO/Login", ""));
        var writer = new StreamWriter(Response.Body);
        Response.ContentType = MediaTypeNames.Text.Html;
        var htmlBody = $@"
                                <html>
                                    <body>
                                        <script type='text/javascript'>
                                            window.location.href = '{returnUrl}login/sso?token={token.AccessToken}'               
                                            window.localStorage.setItem('token','{token.AccessToken}');
                                            window.localStorage.setItem('currentUser','{JsonSerializer.Serialize(user)}');
                                        </script>
                                    </body>
                                </html>";

        writer.WriteAsync(htmlBody).Wait();
        writer.DisposeAsync();

        return NoContent();
    }
}