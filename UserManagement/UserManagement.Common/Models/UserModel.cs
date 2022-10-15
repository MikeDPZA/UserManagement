using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace UserManagement.Common.Models;

[ExcludeFromCodeCoverage]
[Table("um_user")]
public class UserModel: BaseUserManagementModel
{
    public string Lastname { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }

    public string UserIdentifier { get; set; }

    public virtual IList<UserGroupModel> UserGroups { get; set; }
}