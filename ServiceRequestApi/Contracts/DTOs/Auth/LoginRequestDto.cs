using System.ComponentModel.DataAnnotations;

namespace ServiceRequestApi.Contracts.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
