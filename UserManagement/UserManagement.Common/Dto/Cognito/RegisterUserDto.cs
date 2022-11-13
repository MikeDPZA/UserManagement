using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UserManagement.Common.Dto.Cognito;

[ExcludeFromCodeCoverage]
public class RegisterUserDto
{
    [Required] public string Password { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Surname { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }
}