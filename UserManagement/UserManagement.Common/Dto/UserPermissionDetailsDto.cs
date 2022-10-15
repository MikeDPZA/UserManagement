namespace UserManagement.Common.Dto;

public class UserPermissionDetailsDto
{
    public Guid UserId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string UserIdentifier { get; set; }
    public string[] Permissions { get; set; }
}