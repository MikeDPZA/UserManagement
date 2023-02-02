using System.Text.Json.Serialization;
using UserManagement.Common.Attributes.ValidationAttributes;

namespace UserManagement.Common.Dto.Permission;

public class PermissionFilterDto: BaseFilterDto<Permission>
{
    
    [SortKeyValidation(Permission.IdSortKey, Permission.KeySortKey, Permission.DescriptionSortKey, Permission.NameSortKey)]
    public override string SortKey { get; set; }

    [JsonPropertyName("name")]public string Name { get; set; }
    [JsonPropertyName("key")]public string Key { get; set; }
    [JsonPropertyName("description")]public string Description { get; set; }
}