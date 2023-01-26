using System.ComponentModel.DataAnnotations;

namespace UserManagement.GraphQL.Types;

public class UserType: BaseType
{
    public string Lastname { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }

    public string UserIdentifier { get; set; }

    public string FullName => $"{Name} {Lastname}";
    
    public IEnumerable<GroupType> Groups { get; set; } = new List<GroupType>();
}