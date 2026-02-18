using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceRequestApi.Contracts.DTOs.ServiceRequests;
using ServiceRequestApi.Services.ServiceRequests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;



namespace ServiceRequestApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _service;

        public ServiceRequestsController(IServiceRequestService service)
        {
            _service = service;
        }

        // POST: api/servicerequests
       [HttpPost]
        public async Task<ActionResult<ServiceRequestResponseDto>> Create([FromBody] CreateServiceRequestDto dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid token: UserId claim missing.");

            var created = await _service.CreateAsync(dto, userId);

            return CreatedAtAction(nameof(GetById), new { requestId = created.RequestId }, created);
        }


        // GET: api/servicerequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequestResponseDto>>> GetAll()
        {
            var results = await _service.GetAllAsync();
            return Ok(results);
        }

        // GET: api/servicerequests/{requestId}
        [HttpGet("{requestId:int}")]
        public async Task<ActionResult<ServiceRequestResponseDto>> GetById(int requestId)
        {
            var result = await _service.GetByIdAsync(requestId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // PATCH: api/servicerequests/{requestId}/close
        // Optional endpoint (since you already have CloseAsync)
        [Authorize(Roles = "Admin")]
        [HttpPatch("{requestId:int}/close")]
        public async Task<IActionResult> Close(int requestId)
        {
            var ok = await _service.CloseAsync(requestId);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
