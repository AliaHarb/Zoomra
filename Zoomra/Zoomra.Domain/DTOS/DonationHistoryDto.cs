using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class DonationHistoryDto 
    {
        public string HospitalName { get; set; } = null!;
        public DateTime DonationDate { get; set; }
        public string BloodType { get; set; } = null!;
        public int PointsEarned { get; set; }
    }
}
