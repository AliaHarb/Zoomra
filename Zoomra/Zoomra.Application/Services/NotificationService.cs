using AutoMapper;
using Zoomra.Application.Helper;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;
using Zoomra.Domain.Entities;
using Zoomra.Domain.Interfaces;

namespace Zoomra.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> CreateEmergencyNotificationAsync(string bloodType, string hospitalName)
        {
            var notification = new Notification
            {
                Title = "🚨 Urgent Emergency Call",
                Message = $"Hospital {hospitalName} urgently needs blood type {bloodType}. Please visit the hospital and use your National ID to claim your Zomra points!",
                CreatedAt = DateTime.UtcNow,
                Type = "Emergency",
                IsRead = false,
                UserId = "System" // Default ID for system-generated alerts
            };

            // Using the newly added Notifications property in IUnitOfWork
            await _unitOfWork.Notifications.AddAsync(notification);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Failed to create notification.");
        }

        public async Task<Result<IEnumerable<NotificationDto>>> GetDonorNotificationsAsync()
        {
            // Retrieving all notifications directly from the repository
            var notifications = await _unitOfWork.Notifications.GetAllAsync();

            var dtos = notifications
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => {
                    var dto = _mapper.Map<NotificationDto>(n);
                    dto.TimeAgo = GetTimeAgo(n.CreatedAt); // Calculated property for UI
                    return dto;
                });

            return Result<IEnumerable<NotificationDto>>.Success(dtos);
        }

        private string GetTimeAgo(DateTime dateTime)
        {
            var span = DateTime.UtcNow - dateTime;
            if (span.TotalMinutes < 60) return $"{Math.Ceiling(span.TotalMinutes)}m ago";
            if (span.TotalHours < 24) return $"{Math.Ceiling(span.TotalHours)}h ago";
            return $"{Math.Ceiling(span.TotalDays)}d ago";
        }
    }
}