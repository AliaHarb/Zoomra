using System.Threading;
using System.Threading.Tasks;
using Zoomra.Domain.Entities;
using Zoomra.Domain.Interfaces;
using Zoomra.Infrastructure.Data;

namespace Zoomra.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<RefreshToken> RefreshTokens { get; private set; }
        public IBaseRepository<Hospital> Hospitals { get; private set; }
        public IBaseRepository<BloodInventory> BloodInventories { get; private set; }
        public IBaseRepository<InventoryTransaction> InventoryTransactions { get; private set; }
        public IBaseRepository<Donation> Donations { get; private set; }
        public IBaseRepository<EmergencyRequest> EmergencyRequests { get; private set; }
        public IBaseRepository<EmergencyMatch> EmergencyMatches { get; private set; }
        public IBaseRepository<Notification> Notifications { get; private set; }
        public IBaseRepository<Event> Events { get; private set; }
        public IBaseRepository<Reward> Rewards { get; private set; }
        public IBaseRepository<RewardRedemption> RewardRedemptions { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

         
            RefreshTokens = new BaseRepository<RefreshToken>(_context);
            Hospitals = new BaseRepository<Hospital>(_context);
            BloodInventories = new BaseRepository<BloodInventory>(_context);
            InventoryTransactions = new BaseRepository<InventoryTransaction>(_context);
            Donations = new BaseRepository<Donation>(_context);
            EmergencyRequests = new BaseRepository<EmergencyRequest>(_context);
            EmergencyMatches = new BaseRepository<EmergencyMatch>(_context);
            Notifications = new BaseRepository<Notification>(_context);
            Events = new BaseRepository<Event>(_context);
            Rewards = new BaseRepository<Reward>(_context);
            RewardRedemptions = new BaseRepository<RewardRedemption>(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}