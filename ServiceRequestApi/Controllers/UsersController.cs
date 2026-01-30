using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceRequestApi.Contracts.DTOs.Users;
using ServiceRequestApi.Services.Users;

namespace ServiceRequestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser(CreateUserDto dto)
        {
            var createdUser = await _userService.CreateUserAsync(dto);

            return CreatedAtAction(
                nameof(GetUserById),
                new { userId = createdUser.UserId },
                createdUser
            );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
