namespace UserManagement.Services.Attributes;

public class PermissionAttribute: Attribute
{
    public readonly string[] Permissions;
    
    public PermissionAttribute(params string[] permissions)
    {
        Permissions = permissions;
    }
}