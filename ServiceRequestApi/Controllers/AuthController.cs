using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ServiceRequestApi.Contracts.DTOs.Auth;
using ServiceRequestApi.Services.Auth;

namespace ServiceRequestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto dto)
        {
            var response = await _auth.LoginAsync(dto);
            return Ok(response);
        }
    }
}
