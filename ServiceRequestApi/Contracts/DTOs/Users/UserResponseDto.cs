namespace ServiceRequestApi.Contracts.DTOs.Users
{
/// <summary>
/// Data returned by the API when a user is queried.
/// This is what the API RETURNS.
/// </summary>
public class UserResponseDto
{
public int UserId { get; set; }
public string FullName { get; set; }
public string Email { get; set; }
public string Department { get; set; }
}
}