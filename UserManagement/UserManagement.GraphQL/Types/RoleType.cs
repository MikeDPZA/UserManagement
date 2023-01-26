namespace UserManagement.GraphQL.Types;

public class RoleType: BaseType
{
    public IEnumerable<PermissionType> Permissions { get; set; } = new List<PermissionType>();
    public IEnumerable<GroupType> Groups { get; set; } = new List<GroupType>();
}