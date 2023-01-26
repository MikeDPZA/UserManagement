namespace UserManagement.GraphQL.Types;

public class GroupType: BaseType
{
    public IEnumerable<UserType> Users { get; set; } = new List<UserType>();
    public IEnumerable<RoleType> Roles { get; set; } = new List<RoleType>();
}