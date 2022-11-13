using System.Text.Json.Serialization;
using Amazon.CognitoIdentityProvider.Model;

namespace UserManagement.Common.Events;

public class BaseCognitoEvent
{
    [JsonPropertyName("version")]
    public string Version { get; set; }
    
    [JsonPropertyName("region")]
    public string Region { get; set; }
    
    [JsonPropertyName("userPoolId")]
    public string UserPoolId { get; set; }
    
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    
    [JsonPropertyName("callerContext")]
    public CognitoCallerContext CallerContext { get; set; }
    
    [JsonPropertyName("triggerSource")]
    public string TriggerSource { get; set; }
    
}

public class CognitoCallerContext
{
    [JsonPropertyName("awsSdkVersion")]
    public string AwsSdkVersion { get; set; }
    
    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }
}