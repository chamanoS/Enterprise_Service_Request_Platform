namespace ServiceRequestApi.Contracts.DTOs.ServiceRequests
{
    /// <summary>
    /// DTO used when creating a new service request
    /// (input from client)
    /// </summary>
    public class CreateServiceRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        // Who is creating the request
        public int UserId { get; set; }

        // Which department handles it
        public int DepartmentId { get; set; }
    }
}
