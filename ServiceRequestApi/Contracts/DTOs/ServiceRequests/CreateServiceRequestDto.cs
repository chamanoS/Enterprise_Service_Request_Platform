using System.ComponentModel.DataAnnotations;

namespace ServiceRequestApi.Contracts.DTOs.ServiceRequests
{
    public class CreateServiceRequestDto
    {
        [Required, MinLength(3)]
        public string Title { get; set; }

        [Required, MinLength(5)]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }
    }
}
