using Zoomra.Application.Helper;
using Zoomra.Domain.DTOS;

namespace Zoomra.Application.Interfaces
{
   
        public interface INotificationService
        {
            Task<Result<bool>> CreateEmergencyNotificationAsync(string bloodType, string hospitalName);
            Task<Result<IEnumerable<NotificationDto>>> GetDonorNotificationsAsync();
        }
   
}