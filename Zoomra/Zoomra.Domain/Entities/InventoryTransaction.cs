using System;

namespace Zoomra.Domain.Entities
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        public string BloodType { get; set; } = null!;
        public string TransactionType { get; set; } = null!;
        public int UnitsCount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}