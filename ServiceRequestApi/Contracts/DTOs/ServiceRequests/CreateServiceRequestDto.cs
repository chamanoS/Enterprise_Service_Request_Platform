namespace ServiceRequestApi.Contracts.DTOs.ServiceRequests
{
/// <summary>
/// Data required to create a new service request.
/// </summary>
public class CreateServiceRequestDto
{
public string Title { get; set; }
public string Description { get; set; }
public int DepartmentId { get; set; }
}
}