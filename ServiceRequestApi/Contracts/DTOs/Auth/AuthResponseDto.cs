namespace ServiceRequestApi.Contracts.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresInMinutes { get; set; }
    }
}
