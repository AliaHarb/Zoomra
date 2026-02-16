using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoomra.Domain.DTOS;
using Zoomra.Domain.Interfaces;
namespace Zoomra.WebApi.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("create-hospital")]
        public async Task<IActionResult> CreateHospital(AddHospitalDto dto)
        {
            var result = await _adminService.CreateHospitalAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete-hospital/{id}")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            var result = await _adminService.DeleteHospitalAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("all-hospitals")]
        public async Task<IActionResult> GetAllHospitals()
        {
            var result = await _adminService.GetAllHospitalsAsync();
            return Ok(result);
        }

        [HttpPost("add-reward")]
        public async Task<IActionResult> AddReward(AddRewardDto dto)
        {
            var result = await _adminService.AddRewardAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
       

    }
}