using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Common.Models;

[Table("um_role_group")]
public class RoleGroupModel
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RoleGroupId { get; set; }

    [ForeignKey("RoleId")] public virtual RoleModel Role { get; set; }
    public Guid RoleId { get; set; }

    [ForeignKey("GroupId")] public virtual GroupModel Group { get; set; }
    public Guid GroupId { get; set; }
}