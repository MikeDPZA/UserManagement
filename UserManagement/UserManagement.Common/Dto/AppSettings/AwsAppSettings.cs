using System.Text.Json.Serialization;

namespace UserManagement.Common.Dto.AppSettings;

public class AwsAppSettings
{
    [JsonPropertyName("AccountId")] public string AccountId { get; set; }
    [JsonPropertyName("Region")] public string Region { get; set; }
    [JsonPropertyName("UserPoolId")] public string UserPoolId { get; set; }
    [JsonPropertyName("AppClientId")] public string AppClientId { get; set; }
    [JsonPropertyName("AppClientSecret")] public string AppClientSecret { get; set; }

    [JsonPropertyName("UserPoolClientId")] public string UserPoolClientId { get; set; }
    [JsonPropertyName("UserPoolClientSecret")] public string UserPoolClientSecret { get; set; }
    [JsonPropertyName("Domain")] public string Domain { get; set; }
    [JsonPropertyName("RedirectUrl")] public string RedirectUrl { get; set; }
}