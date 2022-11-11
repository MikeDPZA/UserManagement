using System.Text.Json.Serialization;

namespace UserManagement.Common.Dto.User;

public class UserFilterDto
{
    [JsonPropertyName("firstname")] public string Firstname { get; set; } = "";

    [JsonPropertyName("lastname")] public string Lastname { get; set; } = "";

    [JsonPropertyName("email")] public string Email { get; set; } = "";

    [JsonPropertyName("searchText")] public string SearchText { get; set; } = "";

    [JsonPropertyName("userIdentifier")] public string UserIdentifier { get; set; } = "";
}