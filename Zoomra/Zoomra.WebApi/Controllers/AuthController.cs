using Microsoft.AspNetCore.Mvc;
using Zoomra.Domain.DTOS;
using Zoomra.Application.Interfaces;
using Zoomra.Application.Helpers; // تأكدي إن المسار ده صح حسب مشروعك

namespace Zoomra.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            // لو الـ Service بترجع كائن، بنتأكد إنه مش null
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Registration failed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            // لو الـ Service بترجع Token أو User بنرجعه فوراً
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Invalid email or password");
        }
    }
}