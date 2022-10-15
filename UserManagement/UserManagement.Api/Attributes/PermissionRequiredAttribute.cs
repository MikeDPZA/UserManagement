namespace UserManagement.Api.Attributes;

public class PermissionRequiredAttribute: Attribute
{
    public readonly string[] Permissions;
    
    public PermissionRequiredAttribute(params string[] permissions)
    {
        Permissions = permissions;
    }
}