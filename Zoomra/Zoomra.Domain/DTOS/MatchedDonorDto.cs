using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class MatchedDonorDto
    {
        public string DonorId { get; set; }
        public string BloodType { get; set; }
        public double MatchingScore { get; set; }
        public double DistanceKm { get; set; }
    }
}
