using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.Lambda;

namespace UserManagement.CDK.Stacks;

public class CognitoStack : Stack
{
    public readonly UserPool CognitoPool;

    internal CognitoStack(Constructs.Construct scope, string id) : base(scope, id)
    {
        #region Pool

        CognitoPool = new UserPool(this, "UserManagementPool", new UserPoolProps()
        {
            AccountRecovery = AccountRecovery.EMAIL_ONLY,
            Email = UserPoolEmail.WithCognito(),
            Mfa = Mfa.OFF,
            PasswordPolicy = new PasswordPolicy()
            {
                MinLength = 8,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireSymbols = true,
                RequireDigits = true,
                TempPasswordValidity = Duration.Days(3)
            },
            SelfSignUpEnabled = true,
            SignInAliases = new SignInAliases(){Email = true},
            StandardAttributes = new StandardAttributes()
            {
                PreferredUsername = new StandardAttribute(){Mutable = false, Required = false},
                Email = new StandardAttribute() { Mutable = false, Required = true },
                GivenName = new StandardAttribute() { Mutable = true, Required = true },
                FamilyName = new StandardAttribute() { Mutable = true, Required = true },
                ProfilePicture = new StandardAttribute() { Mutable = false, Required = false },
            },
            UserPoolName = "UserManagementPool",
            UserVerification = new UserVerificationConfig()
            {
              EmailStyle  = VerificationEmailStyle.LINK,
              EmailSubject = "Confirm Signup"
            },
            AutoVerify = new AutoVerifiedAttrs()
            {
                Email = true
            }
        });
        
        #endregion

        #region Triggers

        CognitoPool.AddTrigger(UserPoolOperation.POST_CONFIRMATION, new Function(this,
            "CognitoPostConfirmationFunction",
            new FunctionProps()
            {
                Runtime = Runtime.DOTNET_6,
                Handler = "CognitoPostConfirmationFunction::CognitoPostConfirmationFunction.Function::FunctionHandler",
                Code = Code.FromAsset(Path.Join("events", "CognitoPostConfirmationFunction", "src",
                    "CognitoPostConfirmationFunction", "bin", "debug", "net6.0")),
                MemorySize = 256,
                Timeout = Duration.Seconds(30),
                FunctionName = "CognitoPostConfirmationFunction"
            }));

        #endregion

        #region Domain

        CognitoPool.AddDomain("UserManagementPoolDomain", new UserPoolDomainOptions()
        {
            CognitoDomain = new CognitoDomainOptions()
            {
                DomainPrefix = "user-management"
            }
        });

        #endregion

        #region Resource Server

        var userManagementPoolResourceServerOptions = new UserPoolResourceServerProps()
        {
            Identifier = "user-management",
            UserPoolResourceServerName = "user-management",
            Scopes = new ResourceServerScope[]
            {
                new ResourceServerScope(new ResourceServerScopeProps()
                    { ScopeName = "client_credentials", ScopeDescription = "B2B Scope" })
            },
            UserPool = CognitoPool
        };

        var userManagementPoolResourceServer = new UserPoolResourceServer(this, "UserManagementPoolResourceServer",
            userManagementPoolResourceServerOptions);

        #endregion

        #region Clients

        CognitoPool.AddClient("UserManagementUserClient", new UserPoolClientOptions()
        {
            AuthFlows = new AuthFlow()
            {
                UserPassword = true,
                AdminUserPassword = true,
                Custom = true,
                UserSrp = true
            },
            RefreshTokenValidity = Duration.Days(15),
            AccessTokenValidity = Duration.Hours(1),
            IdTokenValidity = Duration.Days(1),
            EnableTokenRevocation = true,
            PreventUserExistenceErrors = true,
            OAuth = new OAuthSettings()
            {
                Flows = new OAuthFlows()
                {
                    AuthorizationCodeGrant = true
                },
                CallbackUrls = new[] { "https://localhost:5001/api/UserManagement/v1/SSO/Login" },
                Scopes = new[] { OAuthScope.EMAIL },
            },
            GenerateSecret = true,
            UserPoolClientName = "UserManagementClient"
        });

        CognitoPool.AddClient("UserManagementB2BClient", new UserPoolClientOptions()
        {
            AuthFlows = new AuthFlow()
            {
                Custom = true,
            },
            RefreshTokenValidity = Duration.Days(15),
            AccessTokenValidity = Duration.Days(1),
            EnableTokenRevocation = true,
            OAuth = new OAuthSettings()
            {
                Flows = new OAuthFlows()
                {
                    ClientCredentials = true
                },
                CallbackUrls = new[] { "https://localhost:5001/api/UserManagement/v1/SSO/Login" },
                Scopes = new OAuthScope[]
                {
                    OAuthScope.ResourceServer(userManagementPoolResourceServer, new ResourceServerScope(
                        new ResourceServerScopeProps()
                        {
                            ScopeName = "client_credentials",
                            ScopeDescription = "B2B Scope"
                        })),
                }
            },
            GenerateSecret = true,
            UserPoolClientName = "UserManagementB2BClient"
        });

        #endregion
    }
}