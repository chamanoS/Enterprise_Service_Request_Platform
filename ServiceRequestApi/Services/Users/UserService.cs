using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceRequestApi.Contracts.DTOs.Users;
using ServiceRequestApi.Infrastructure.Data;
using ServiceRequestApi.Models.Entities;

namespace ServiceRequestApi.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new InvalidOperationException("Username is required.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new InvalidOperationException("Password is required.");

            // Optional: prevent duplicate usernames
            var usernameExists = await _context.Users.AnyAsync(u => u.Username == dto.Username);
            if (usernameExists)
                throw new InvalidOperationException("Username already exists.");

            // Create entity
            var user = new User
            {
                Username = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DepartmentId = dto.DepartmentId,
                IsActive = true
            };

            // Hash password (never store plain passwords)
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return DTO (load department name)
            return await _context.Users
                .Include(u => u.Department)
                .Where(u => u.UserId == user.UserId)
                .Select(u => new UserResponseDto
                {
                    UserId = u.UserId,
                    FullName = u.FirstName + " " + u.LastName,
                    Email = u.Email,
                    Department = u.Department.DepartmentName
                })
                .FirstAsync();
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Department)
                .Select(u => new UserResponseDto
                {
                    UserId = u.UserId,
                    FullName = u.FirstName + " " + u.LastName,
                    Email = u.Email,
                    Department = u.Department.DepartmentName
                })
                .ToListAsync();
        }

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
                    Department = u.Department.DepartmentName
                })
                .FirstOrDefaultAsync();
        }
    }
}
