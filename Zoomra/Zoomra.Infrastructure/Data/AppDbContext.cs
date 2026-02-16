using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Zoomra.Domain.Entities;

namespace Zoomra.Infrastructure.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

      
      
            public DbSet<RefreshToken> RefreshTokens { get; set; }
            public DbSet<Hospital> Hospitals { get; set; }
            public DbSet<BloodInventory> BloodInventories { get; set; }
            public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
            public DbSet<Donation> Donations { get; set; }
            public DbSet<EmergencyRequest> EmergencyRequests { get; set; }
            public DbSet<EmergencyMatch> EmergencyMatches { get; set; }
            public DbSet<Notification> Notifications { get; set; }
            public DbSet<Event> Events { get; set; }
            public DbSet<Reward> Rewards { get; set; }
            public DbSet<RewardRedemption> RewardRedemptions { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder); 

              

                // 1. علاقة المستشفى بالأدمن
                builder.Entity<Hospital>()
                    .HasOne(h => h.Admin)
                    .WithMany()
                    .HasForeignKey(h => h.AdminId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 2. علاقات التبرع (Donation)
                builder.Entity<Donation>()
                    .HasOne(d => d.Donor)
                    .WithMany(u => u.Donations)
                    .HasForeignKey(d => d.DonorId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.Entity<Donation>()
                    .HasOne(d => d.Hospital)
                    .WithMany(h => h.Donations)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.Restrict);

              
                builder.Entity<EmergencyMatch>()
                    .HasOne(m => m.Donor)
                    .WithMany(u => u.EmergencyMatches)
                    .HasForeignKey(m => m.DonorId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.Entity<EmergencyMatch>()
                    .HasOne(m => m.EmergencyRequest)
                    .WithMany(r => r.EmergencyMatches)
                    .HasForeignKey(m => m.RequestId)
                    .OnDelete(DeleteBehavior.Cascade);


             builder.Entity<RewardRedemption>(entity =>
            {
                entity.HasKey(rr => rr.Id);

                entity.HasOne(rr => rr.Donor)
                      .WithMany(u => u.RewardRedemptions)
                      .HasForeignKey(rr => rr.DonorId);

                entity.HasOne(rr => rr.Reward)
                      .WithMany(r => r.RewardRedemptions)
                      .HasForeignKey(rr => rr.RewardId);
            });

            // 5. علاقات الإشعارات والتوكنز
            builder.Entity<Notification>()
                    .HasOne(n => n.User)
                    .WithMany(u => u.Notifications)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<RefreshToken>()
                    .HasOne(r => r.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }