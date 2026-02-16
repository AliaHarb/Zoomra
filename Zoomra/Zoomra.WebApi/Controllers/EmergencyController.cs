using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;

namespace Zoomra.WebApi.Controllers
{
    [Authorize(Roles = "Hospital")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyController : ControllerBase
    {
        private readonly IEmergencyService _emergencyService;

        public EmergencyController(IEmergencyService emergencyService)
        {
            _emergencyService = emergencyService;
        }

        [HttpPost("raise")]
        public async Task<IActionResult> RaiseEmergency([FromBody] CreateEmergencyCallDto dto)
        {
            var result = await _emergencyService.RaiseEmergencyAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}