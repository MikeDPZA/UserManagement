using System.Text.Json.Serialization;

namespace UserManagement.Common.Events;

public class CognitoPostConfirmationEvent: BaseCognitoEvent
{
    [JsonPropertyName("request")]
    public CognitoRequest Request { get; set; }
    
    [JsonPropertyName("response")]
    public object Response { get; set; } = default!;
}

public class CognitoRequest
{
    [JsonPropertyName("userAttributes")]
    public CognitoUserAttributes UserAttributes { get; set; }
}

public class CognitoUserAttributes
{
    [JsonPropertyName("sub")]
    public string Sub { get; set; }
    
    [JsonPropertyName("email_verified")]
    public string EmailVerified { get; set; }

    [JsonPropertyName("cognito:user_status")]
    public string CognitoUserStatus { get; set; }

    [JsonPropertyName("cognito:email_alias")]
    public string CognitoEmailAlias { get; set; }
    
    [JsonPropertyName("given_name")]
    public string Name { get; set; }
    
    [JsonPropertyName("family_name")]
    public string Surname { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}
