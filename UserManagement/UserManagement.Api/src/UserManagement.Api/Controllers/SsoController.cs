using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Web;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
public class SsoController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private ILogger<SsoController> _logger;
    private readonly AwsAppSettings _awsSettings;
    private readonly IUserControllerService _userControllerService;
    private readonly IOAuthFacade _oAuthFacade;


    /* This is the constructor for the SsoController class. It is initializing the class variables. */
    public SsoController(IIdentityService identityService,
        ILogger<SsoController> logger,
        IOptions<AwsAppSettings> awsSettings,
        IUserControllerService userControllerService,
        IOAuthFacade oAuthFacade
    )
    {
        _identityService = identityService;
        _logger = logger;
        _userControllerService = userControllerService;
        _oAuthFacade = oAuthFacade;
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
        try
        {
            var oAuthResult = await _oAuthFacade.GetUserToken(
                code,
                _awsSettings.AppClientId,
                _awsSettings.AppClientSecret,
                _awsSettings.Domain,
                _awsSettings.RedirectUrl
            );

            var jwt = new JwtSecurityToken(oAuthResult.AccessToken);
            var user = _userControllerService.GetUser(Guid.Parse(jwt.Subject));

            return RedirectAfterAuth(user, oAuthResult);
        }
        catch (Exception e)
        {
            LambdaLogger.Log($"Failed Login: {e}");
            return BadRequest(e);
        }
    }

    private IActionResult RedirectAfterAuth(User user, OAuthTokenResponse token)
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