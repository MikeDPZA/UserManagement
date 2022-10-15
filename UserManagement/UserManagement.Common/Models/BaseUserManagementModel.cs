using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Common.Models;

public class BaseUserManagementModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public string Description { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}