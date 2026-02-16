using System.Collections.Generic;
using Zoomra.Domain.Entities;

namespace Zoomra.Domain.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? City { get; set; }
        public string? ContactPhone { get; set; }

        public string? AdminId { get; set; }
        public ApplicationUser? Admin { get; set; }

        public ICollection<BloodInventory> BloodInventories { get; set; } = new List<BloodInventory>();
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
        public ICollection<EmergencyRequest> EmergencyRequests { get; set; } = new List<EmergencyRequest>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}