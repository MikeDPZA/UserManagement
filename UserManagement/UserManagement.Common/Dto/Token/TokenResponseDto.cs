namespace UserManagement.Common.Dto.Token;

public class TokenResponseDto
{
    public string RefreshToken { get; set; }
    public string Token { get; set; }
    public DateTimeOffset Expiry { get; set; }
}