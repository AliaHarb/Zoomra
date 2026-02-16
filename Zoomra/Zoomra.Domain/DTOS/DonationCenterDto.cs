using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class DonationCenterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // Dar Al-Fouad
        public double Rate { get; set; } // 4.5
        public string Address { get; set; } = null!;
        public string WorkingHours { get; set; } = null!; // Open until 8:00 PM
        public string? ImageUrl { get; set; }
    }
}
