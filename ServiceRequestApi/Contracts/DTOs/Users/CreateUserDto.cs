namespace ServiceRequestApi.Contracts.DTOs.Users
{
/// <summary>
/// Data required to create a new user.
/// This is what the API ACCEPTS.
/// </summary>
public class CreateUserDto
{
public string FirstName { get; set; }
public string LastName { get; set; }
public string Email { get; set; }
public int DepartmentId { get; set; }
}
}