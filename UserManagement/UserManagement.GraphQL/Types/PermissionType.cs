namespace UserManagement.GraphQL.Types;

public class PermissionType: BaseType
{
    public string Key { get; set; }
    public IEnumerable<RoleType> Roles { get; set; } = new List<RoleType>();
}