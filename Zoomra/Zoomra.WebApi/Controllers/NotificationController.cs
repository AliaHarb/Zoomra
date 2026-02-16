using Microsoft.AspNetCore.Mvc;
using Zoomra.Application.Interfaces;

namespace Zoomra.WebApi.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class NotificationController : ControllerBase
        {
            private readonly INotificationService _notificationService;

            public NotificationController(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            [HttpPost("trigger-emergency")]
            public async Task<IActionResult> TriggerEmergency(string bloodType, string hospitalName)
            {
                var result = await _notificationService.CreateEmergencyNotificationAsync(bloodType, hospitalName);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }

            [HttpGet("my-notifications")]
            public async Task<IActionResult> GetMyNotifications()
            {
                var result = await _notificationService.GetDonorNotificationsAsync();
                return Ok(result);
            }
        }
}
