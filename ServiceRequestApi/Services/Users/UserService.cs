using System.Collections.Generic;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceRequestApi.Contracts.DTOs.Users;
using ServiceRequestApi.Infrastructure.Data;
using ServiceRequestApi.Models.Entities;


namespace ServiceRequestApi.Services.Users
{
    /// <summary>
    /// Implements user-related business logic.
    /// Handles mapping between DTOs and entities.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;


        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DepartmentId = dto.DepartmentId
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            var department = await _context.Departments
            .Where(d => d.DepartmentId == user.DepartmentId)
            .Select(d => d.Name)
            .FirstAsync();


            return new UserResponseDto
            {
                UserId = user.UserId,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Department = department
            };
        }


        // -----------------------
        // Get All Users
        // -----------------------
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context.Users
            .Include(u => u.Department)
            .Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName,
                Email = u.Email,
                Department = u.Department.Name
            })
            .ToListAsync();
        }


        // -----------------------
        // Get User By Id
        // -----------------------
        public async Task<UserResponseDto> GetUserByIdAsync(int userId)
        {
            return await _context.Users
            .Include(u => u.Department)
            .Where(u => u.UserId == userId)
            .Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                FullName = u.FirstName + " " + u.LastName,
                Email = u.Email,
                Department = u.Department.Name
            })
            .FirstOrDefaultAsync();
        }
    }
}