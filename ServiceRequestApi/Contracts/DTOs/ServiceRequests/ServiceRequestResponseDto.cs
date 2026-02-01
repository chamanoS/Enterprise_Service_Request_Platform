using System;

namespace ServiceRequestApi.Contracts.DTOs.ServiceRequests
{
    /// <summary>
    /// DTO returned to the client when reading service requests
    /// </summary>
    public class ServiceRequestResponseDto
    {
        public int ServiceRequestId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        public string Status { get; set; }
        public string Department { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
