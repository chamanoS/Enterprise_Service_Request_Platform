using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceRequestApi.Contracts.DTOs.ServiceRequests;

namespace ServiceRequestApi.Services.ServiceRequests
{
    /// <summary>
    /// Contract for service request business logic.
    /// Controllers depend on this interface, not implementations.
    /// </summary>
    public interface IServiceRequestService
    {
        // Create a new service request
        Task<ServiceRequestResponseDto> CreateAsync(CreateServiceRequestDto dto, int userId);

        // Read operations
        Task<IEnumerable<ServiceRequestResponseDto>> GetAllAsync();
        Task<ServiceRequestResponseDto?> GetByIdAsync(int serviceRequestId);

        // Optional (for later phases)
        Task<bool> CloseAsync(int serviceRequestId);
    }
}
