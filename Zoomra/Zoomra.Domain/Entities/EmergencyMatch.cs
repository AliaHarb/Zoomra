using Zoomra.Domain.Entities;

namespace Zoomra.Domain.Entities
{
    public class EmergencyMatch
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public EmergencyRequest? EmergencyRequest { get; set; }

        public string DonorId { get; set; } = null!;
        public ApplicationUser? Donor { get; set; }

        public double MatchScore { get; set; }
        public double DistanceKm { get; set; }
        public bool HasResponded { get; set; } = false;
    }
}