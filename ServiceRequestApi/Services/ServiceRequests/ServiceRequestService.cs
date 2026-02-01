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
    /// <summary>
    /// Implements business logic for Service Requests.
    /// This layer coordinates validation, persistence, and mapping.
    /// </summary>
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------------------------------
        // CREATE
        // ---------------------------------
        public async Task<ServiceRequestResponseDto> CreateAsync(CreateServiceRequestDto dto)
        {
            // Validate User
            var user = await _context.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.UserId == dto.UserId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Validate Department
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == dto.DepartmentId);

            if (department == null)
            {
                throw new InvalidOperationException("Department not found.");
            }

            // Default status: Open
            var openStatus = await _context.RequestStatuses
                .FirstAsync(rs => rs.StatusName == "Open");

            var serviceRequest = new ServiceRequest
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId,
                DepartmentId = dto.DepartmentId,
                RequestStatusId = openStatus.RequestStatusId,
                CreatedDate = DateTime.UtcNow
            };

            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();

            return new ServiceRequestResponseDto
            {
                ServiceRequestId = serviceRequest.ServiceRequestId,
                Title = serviceRequest.Title,
                Description = serviceRequest.Description,
                CreatedDate = serviceRequest.CreatedDate,
                Status = openStatus.StatusName,
                Department = department.DepartmentName,
                UserId = user.UserId,
                Username = user.Username
            };
        }

        // ---------------------------------
        // READ ALL
        // ---------------------------------
        public async Task<IEnumerable<ServiceRequestResponseDto>> GetAllAsync()
        {
            return await _context.ServiceRequests
                .Include(sr => sr.RequestStatus)
                .Include(sr => sr.Department)
                .Include(sr => sr.User)
                .Select(sr => new ServiceRequestResponseDto
                {
                    ServiceRequestId = sr.ServiceRequestId,
                    Title = sr.Title,
                    Description = sr.Description,
                    CreatedDate = sr.CreatedDate,
                    ClosedDate = sr.ClosedDate,
                    Status = sr.RequestStatus.StatusName,
                    Department = sr.Department.DepartmentName,
                    UserId = sr.User.UserId,
                    Username = sr.User.Username
                })
                .ToListAsync();
        }

        // ---------------------------------
        // READ BY ID
        // ---------------------------------
        public async Task<ServiceRequestResponseDto?> GetByIdAsync(int serviceRequestId)
        {
            return await _context.ServiceRequests
                .Include(sr => sr.RequestStatus)
                .Include(sr => sr.Department)
                .Include(sr => sr.User)
                .Where(sr => sr.ServiceRequestId == serviceRequestId)
                .Select(sr => new ServiceRequestResponseDto
                {
                    ServiceRequestId = sr.ServiceRequestId,
                    Title = sr.Title,
                    Description = sr.Description,
                    CreatedDate = sr.CreatedDate,
                    ClosedDate = sr.ClosedDate,
                    Status = sr.RequestStatus.StatusName,
                    Department = sr.Department.DepartmentName,
                    UserId = sr.User.UserId,
                    Username = sr.User.Username
                })
                .FirstOrDefaultAsync();
        }

        // ---------------------------------
        // CLOSE REQUEST (optional)
        // ---------------------------------
        public async Task<bool> CloseAsync(int serviceRequestId)
        {
            var serviceRequest = await _context.ServiceRequests
                .Include(sr => sr.RequestStatus)
                .FirstOrDefaultAsync(sr => sr.ServiceRequestId == serviceRequestId);

            if (serviceRequest == null)
            {
                return false;
            }

            var closedStatus = await _context.RequestStatuses
                .FirstAsync(rs => rs.StatusName == "Closed");

            serviceRequest.RequestStatusId = closedStatus.RequestStatusId;
            serviceRequest.ClosedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
