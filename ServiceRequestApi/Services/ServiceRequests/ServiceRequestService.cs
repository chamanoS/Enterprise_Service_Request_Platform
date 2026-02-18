using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceRequestApi.Contracts.DTOs.ServiceRequests;
using ServiceRequestApi.Infrastructure.Data;
using ServiceRequestApi.Models.Entities;

namespace ServiceRequestApi.Services.ServiceRequests
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        // -----------------------
        // CREATE
        // -----------------------
        public async Task<ServiceRequestResponseDto> CreateAsync(CreateServiceRequestDto dto, int userId)
        {
            // Validate user exists
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists)
                throw new InvalidOperationException("User not found.");

            // Validate department exists
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == dto.DepartmentId);

            if (department == null)
                throw new InvalidOperationException("Department not found.");

            // Default status: New (matches your seeding)
            var newStatus = await _context.RequestStatuses
                .FirstAsync(s => s.StatusName == "New");

            var serviceRequest = new ServiceRequest
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId,
                DepartmentId = dto.DepartmentId,
                StatusId = newStatus.StatusId,
                CreatedDate = DateTime.UtcNow
            };

            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();

            // Load username (keep it simple for now)
            var username = await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => u.Username)
                .FirstAsync();

            return new ServiceRequestResponseDto
            {
                RequestId = serviceRequest.RequestId,
                Title = serviceRequest.Title,
                Description = serviceRequest.Description,
                CreatedDate = serviceRequest.CreatedDate,
                Status = newStatus.StatusName,
                Department = department.DepartmentName,   // make sure Department has DepartmentName
                Username = username
            };
        }

        // -----------------------
        // READ ALL
        // -----------------------
        public async Task<IEnumerable<ServiceRequestResponseDto>> GetAllAsync()
        {
            return await _context.ServiceRequests
                .Include(sr => sr.Status)
                .Include(sr => sr.Department)
                .Include(sr => sr.User)
                .Select(sr => new ServiceRequestResponseDto
                {
                    RequestId = sr.RequestId,
                    Title = sr.Title,
                    Description = sr.Description,
                    CreatedDate = sr.CreatedDate,
                    Status = sr.Status.StatusName,
                    Department = sr.Department.DepartmentName,
                    UserId = sr.UserId,
                    Username = sr.User.Username
                })
                .ToListAsync();
        }

        // -----------------------
        // READ BY ID
        // -----------------------
        public async Task<ServiceRequestResponseDto?> GetByIdAsync(int requestId)
        {
            return await _context.ServiceRequests
                .Include(sr => sr.Status)
                .Include(sr => sr.Department)
                .Include(sr => sr.User)
                .Where(sr => sr.RequestId == requestId)
                .Select(sr => new ServiceRequestResponseDto
                {
                    RequestId = sr.RequestId,
                    Title = sr.Title,
                    Description = sr.Description,
                    CreatedDate = sr.CreatedDate,
                    Status = sr.Status.StatusName,
                    Department = sr.Department.DepartmentName,
                    UserId = sr.UserId,
                    Username = sr.User.Username
                })
                .FirstOrDefaultAsync();
        }

        // -----------------------
        // CLOSE (optional)
        // -----------------------
        public async Task<bool> CloseAsync(int requestId)
        {
            var sr = await _context.ServiceRequests.FirstOrDefaultAsync(x => x.RequestId == requestId);
            if (sr == null) return false;

            var closed = await _context.RequestStatuses.FirstAsync(s => s.StatusName == "Closed");

            sr.StatusId = closed.StatusId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
