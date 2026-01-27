namespace ServiceRequestApi.Contracts.DTOs.ServiceRequests
{
/// <summary>
/// Data returned when a service request is queried.
/// </summary>
public class ServiceRequestResponseDto
{
public int RequestId { get; set; }
public string Title { get; set; }
public string Status { get; set; }
public string Department { get; set; }
public string CreatedBy { get; set; }
public string CreatedDate { get; set; }
}
}