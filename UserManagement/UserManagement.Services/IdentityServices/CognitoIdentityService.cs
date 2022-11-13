using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.Extensions.Options;
using UserManagement.Common.Dto.AppSettings;
using UserManagement.Common.Dto.Cognito;
using UserManagement.Common.Dto.Token;
using UserManagement.Common.Dto.User;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.IdentityServices;

public class CognitoIdentityService : IIdentityService
{
    private readonly IAmazonCognitoIdentityProvider _identityProvider;
    private readonly CognitoUserPool _userPool;
    private readonly AwsAppSettings _awsOptions;

    public CognitoIdentityService(IAmazonCognitoIdentityProvider identityProvider, CognitoUserPool userPool,
        IOptions<AwsAppSettings> awsOptions)
    {
        _identityProvider = identityProvider;
        _userPool = userPool;
        _awsOptions = awsOptions.Value;
    }

    public Task UpdateUser()
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser()
    {
        throw new NotImplementedException();
    }

    public Task GetUser()
    {
        throw new NotImplementedException();
    }

    public Task ForgotPassword()
    {
        throw new NotImplementedException();
    }

    public async Task<OAuthTokenResponse> Login(UserLoginDto loginDto)
    {
        var user = new CognitoUser(loginDto.Email,
            _awsOptions.AppClientId,
            _userPool, _identityProvider,
            _awsOptions.AppClientSecret
        );
        var authRequest = new InitiateSrpAuthRequest() { Password = loginDto.Password };
        var authResponse = await user.StartWithSrpAuthAsync(authRequest);
        if (authResponse.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
        {
            // throw new UpdatePasswordException();
        }

        return new OAuthTokenResponse()
        {
            RefreshToken = authResponse.AuthenticationResult.RefreshToken,
            AccessToken = authResponse.AuthenticationResult.AccessToken,
            ExpiresIn = authResponse.AuthenticationResult.ExpiresIn,
            IdToken = authResponse.AuthenticationResult.IdToken,
            TokenType = authResponse.AuthenticationResult.TokenType
        };
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public async Task SignUp(RegisterUserDto registerDto)
    {
        await _userPool.SignUpAsync(
            registerDto.Email,
            registerDto.Password,
            new Dictionary<string, string>()
            {
                { "email", registerDto.Email },
                { "given_name", registerDto.Name },
                { "family_name", registerDto.Surname },
                { "picture", "" },
            }, new Dictionary<string, string>());
    }
}