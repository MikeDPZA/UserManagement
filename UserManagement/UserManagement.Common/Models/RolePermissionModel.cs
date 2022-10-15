using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Common.Models;

[Table("um_role_permission")]
public class RolePermissionModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
    public Guid RolePermissionId { get; set; }

    [ForeignKey("RoleId")] public virtual RoleModel Role { get; set; }
    public Guid RoleId { get; set; }

    [ForeignKey("PermissionId")] public virtual PermissionModel Permission { get; set; }
    public Guid PermissionId { get; set; }
}