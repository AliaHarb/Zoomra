using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Zoomra.Application.Interfaces;

namespace Zoomra.WebApi.Controllers
{
    [Authorize] // لازم يكون مسجل دخول
    [ApiController]
    [Route("api/[controller]")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // هاتي الـ ID من التوكن
            var result = await _donorService.GetDashboardDataAsync(userId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("redeem/{rewardId}")]
        public async Task<IActionResult> Redeem(int rewardId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _donorService.RedeemRewardAsync(userId, rewardId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
       
        
        
          
           
        
    }
}
