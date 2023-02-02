using System.Linq.Expressions;
using System.Text.Json.Serialization;
using LinqKit;
using UserManagement.Common.Attributes.ValidationAttributes;
using UserManagement.Common.Models;

namespace UserManagement.Common.Dto.User;

public class UserFilterDto: BaseFilterDto<UserModel>
{
    [JsonPropertyName("firstname")] public string Firstname { get; set; } = "";

    [JsonPropertyName("lastname")] public string Lastname { get; set; } = "";

    [JsonPropertyName("email")] public string Email { get; set; } = "";

    [JsonPropertyName("userIdentifier")] public string UserIdentifier { get; set; } = "";

    [SortKeyValidation(User.IdSortKey, User.FirstnameSortKey, User.LastnameSortKey, User.EmailSortKey, User.UserIdentifierSortKey)]
    public override string SortKey { get; set; } = User.FirstnameSortKey;
}