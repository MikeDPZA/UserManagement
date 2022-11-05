using System.Diagnostics.CodeAnalysis;

namespace UserManagement.Common.Dto.User;

[ExcludeFromCodeCoverage]
public class User
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string FullName => Firstname + " " + Lastname;
    public string Email { get; set; }
    public string UserIdentifier { get; set; }
}