using System.ComponentModel.DataAnnotations;

namespace ServiceRequestApi.Contracts.DTOs.Users
{
    public class CreateUserDto
    {
        [Required, MinLength(3)]
        public string Username { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }
    }
}
