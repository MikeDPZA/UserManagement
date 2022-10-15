using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Common.Models;

[Table("um_user_group")]
public class UserGroupModel
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserGroupId { get; set; }

    [ForeignKey("UserId")] public virtual UserModel User { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey("GroupId")] public virtual GroupModel Group { get; set; }
    public Guid GroupId { get; set; }
    
}