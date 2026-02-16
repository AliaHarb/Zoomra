using System;
using System.Threading;
using System.Threading.Tasks;
using Zoomra.Domain.Entities;

namespace Zoomra.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<RefreshToken> RefreshTokens { get; }
        IBaseRepository<Hospital> Hospitals { get; }
        IBaseRepository<BloodInventory> BloodInventories { get; }
        IBaseRepository<InventoryTransaction> InventoryTransactions { get; }
        IBaseRepository<Donation> Donations { get; }
        IBaseRepository<EmergencyRequest> EmergencyRequests { get; }
        IBaseRepository<EmergencyMatch> EmergencyMatches { get; }
        IBaseRepository<Notification> Notifications { get; }
        IBaseRepository<Event> Events { get; }
        IBaseRepository<Reward> Rewards { get; }
        IBaseRepository<RewardRedemption> RewardRedemptions { get; }
        

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}