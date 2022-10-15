using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Common.Models;

[Table("um_role")]
public class RoleModel: BaseUserManagementModel
{
    public virtual IList<RolePermissionModel> RolePermissions { get; set; }
    public virtual IList<RoleGroupModel> RoleGroups { get; set; }
}