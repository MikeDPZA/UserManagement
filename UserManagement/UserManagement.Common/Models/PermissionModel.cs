using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Common.Models;

[Table("um_permission")]
public class PermissionModel: BaseUserManagementModel
{
    public string Key { get; set; }
    public virtual IList<RolePermissionModel> RolePermissions { get; set; }
}