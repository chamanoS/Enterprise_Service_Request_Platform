using System;

namespace ServiceRequestApi.Contracts.DTOs.ServiceRequests
{
    public class ServiceRequestResponseDto
    {
        public int RequestId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; }
        public string Department { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
