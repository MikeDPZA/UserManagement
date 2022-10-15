using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Common.Models;

[Table("um_group")]
public class GroupModel: BaseUserManagementModel
{
    public virtual IList<UserGroupModel> UserGroups { get; set; }
    public virtual IList<RoleGroupModel> RoleGroups { get; set; }
}