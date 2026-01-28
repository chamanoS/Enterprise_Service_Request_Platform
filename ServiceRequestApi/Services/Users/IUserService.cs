using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceRequestApi.Contracts.DTOs.Users;


namespace ServiceRequestApi.Services.Users
{
/// <summary>
/// Defines user-related business operations.
/// Controllers depend on this abstraction.
/// </summary>
public interface IUserService
{
Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
Task<UserResponseDto> GetUserByIdAsync(int userId);
}
}