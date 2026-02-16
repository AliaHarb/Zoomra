using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomra.Application.Helper;
using Zoomra.Domain.DTOS;

namespace Zoomra.Application.Interfaces
{
    public interface IDonorService
    {

        Task<Result<DonorDashboardDto>> GetDashboardDataAsync(string userId);
        Task<Result<IEnumerable<DonationHistoryDto>>> GetDonationHistoryAsync(string userId);

        // شاشة الـ Rewards (عرض واستبدال)
        Task<Result<UserRewardsSummaryDto>> GetRewardsSummaryAsync(string userId);
        Task<Result<IEnumerable<RewardDto>>> GetAvailableRewardsAsync();

        // الفعل الأساسي: استبدال النقاط
        Task<Result<bool>> RedeemRewardAsync(string userId, int rewardId);

        // شاشة الـ Map والـ Emergencies
        Task<Result<IEnumerable<DonationCenterDto>>> GetNearbyCentersAsync();
        Task<Result<IEnumerable<EmergencyCallDto>>> GetActiveEmergenciesAsync();


    }


}
