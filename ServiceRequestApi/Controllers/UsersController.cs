using Microsoft.AspNetCore.Mvc;
using ServiceRequestApi.Infrastructure.Data;

namespace ServiceRequestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
