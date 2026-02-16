using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class ConfirmDonationDto
    {
        public string DonorId { get; set; } = null!;
        public int HospitalId { get; set; }
        public string BloodType { get; set; } = null!;
        public int UnitsCount { get; set; } = 1;
        public int? EmergencyCallId { get; set; }
        public bool ShouldCloseCall
        {
            get; set;
        }
    }
}
